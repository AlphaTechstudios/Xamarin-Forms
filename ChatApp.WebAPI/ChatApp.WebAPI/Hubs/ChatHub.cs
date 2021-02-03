using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.WebAPI.Hubs
{
    public class ChatHub: Hub
    {

        public async Task SendMessage(string userId, string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", userId, message);
        }
    }
}
