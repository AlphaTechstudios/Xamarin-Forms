using System;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Interfaces
{
    public interface IChatService
    {
        Task Connect(string userEmail);
        Task Disconnect(string userEmail);
        Task SendMessage(string userId, string message, bool isPrivate = false);
        void ReceiveMessage(Action<string, string> GetMessageAndUser, bool isPrivate = false);
        void ReceivePrivateVideoCall(Action<string> GetVideoCall);
        Task RejectVideoCall(string currentUser, string friendUser);
        Task AcceptVideoCall(string currentUser, string friendUser);
        Task CallFriend(string friendEmail);
        void AcceptVideoCallByFriend(Action<string, string> VideoCallAcceptedByFriend);
    }
}
