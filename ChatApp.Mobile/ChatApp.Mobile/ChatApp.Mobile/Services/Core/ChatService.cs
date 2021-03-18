using ChatApp.Mobile.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Core
{
    public class ChatService: IChatService
    {
        private readonly HubConnection hubConnection;
        public ChatService()
        {
            hubConnection = new HubConnectionBuilder().WithUrl("http://192.168.1.36:45459/chathub").Build();
        }

        public async Task Connect(string userEmail)
        {
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("OnConnect", userEmail);
        }

        public async Task Disconnect(string userEmail)
        {
            await hubConnection.InvokeAsync("OnDisconnect", userEmail);
            await hubConnection.StopAsync();

        }

        public async Task SendMessage(string userId, string message, bool isPrivate= false)
        {
            if(isPrivate)
            {
                await hubConnection.InvokeAsync("SendPrivateMessage", userId, message);
            }
            else
            {
                await hubConnection.InvokeAsync("SendMessage", userId, message);
            }
        }

        public void ReceiveMessage(Action<string, string> GetMessageAndUser, bool isPrivate = false)
        {
            if(isPrivate)
            {
                hubConnection.On("ReceivePrivateMessage", GetMessageAndUser);
            }
            else
            {
                hubConnection.On("ReceiveMessage", GetMessageAndUser);
            }
        }
    }
}
