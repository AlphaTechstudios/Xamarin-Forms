using ChatApp.Managers.Interfaces;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUsersManager usersManager;
        private readonly IConversationsManager conversationsManager;

        public ChatHub(IUsersManager usersManager, IConversationsManager conversationsManager)
        {
            this.usersManager = usersManager;
            this.conversationsManager = conversationsManager;
        }

        public async Task SendMessage(string userId, string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", userId, message);
        }

        public async Task SendPrivateMessage(string userEmail, string message)
        {

            var senderUser = usersManager.GetUserByConnectionId(Context.ConnectionId);
            var friend = usersManager.GetUserByEmail(userEmail);
            var friendConnections = friend.Connections.Where(x => x.IsConnected);
            foreach (var connection in friendConnections)
            {
                await Clients.Client(connection.ConnectionID).SendAsync("ReceivePrivateMessage", userEmail, message);
            }
            // Inser in to database..
            var conversationModel = conversationsManager.GetConversationByUsersId(senderUser.ID, friend.ID);

            if (conversationModel == null)
            {
                var conversationId = conversationsManager.AddOrUpdateConversation(senderUser.ID, friend.ID);
                conversationsManager.AddReply(message, conversationId, senderUser.ID);
            }
            else
            {
                conversationsManager.AddReply(message, conversationModel.ID, senderUser.ID);
            }
        }

        public async Task CallFriendAsync(string userEmail)
        {
            var senderUser = usersManager.GetUserByConnectionId(Context.ConnectionId);
            var friend = usersManager.GetUserByEmail(userEmail);
            var friendConnections = friend.Connections.Where(x => x.IsConnected && x.IsAvailable);
            
            foreach (var connection in friendConnections)
            {
                await Clients.Client(connection.ConnectionID).SendAsync("ReceivePrivateVideoCall", senderUser.Email);
            }          
        }

        public async Task AcceptVideoCall(string currentUser, string friendUser)
        {
            var senderUser = usersManager.GetUserByConnectionId(Context.ConnectionId);
            var friend = usersManager.GetUserByEmail(friendUser);
            var friendConnections = friend.Connections.Where(x => x.IsConnected);
            foreach (var connection in friendConnections)
            {
                await Clients.Client(connection.ConnectionID).SendAsync("AcceptVideoCallByFriend", currentUser, friendUser);
            }
        }

        public async Task RejectVideoCall(string currentUser, string friendUser)
        {
            var senderUser = usersManager.GetUserByConnectionId(Context.ConnectionId);
            var friend = usersManager.GetUserByEmail(friendUser);
            var friendConnections = friend.Connections.Where(x => x.IsConnected);
            foreach (var connection in friendConnections)
            {
                await Clients.Client(connection.ConnectionID).SendAsync("RejectVideoCallByFriend", currentUser, friendUser);
            }
        }



        public async Task OnConnect(string userEmail)
        {
            var user = usersManager.GetUserByEmail(userEmail);
            usersManager.AddUserConnections(new ConnectionModel
            {
                ConnectionID = Context.ConnectionId,
                IsConnected = true,
                UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"],
                UserID = user.ID
            });

            await base.OnConnectedAsync();
        }

        public async Task OnDisconnect(string userEmail)
        {
            var user = usersManager.GetUserByEmail(userEmail);
            usersManager.UpdateUserConnectionsStatus(user.ID, false, Context.ConnectionId);
            await base.OnDisconnectedAsync(null);
        }
    }
}

