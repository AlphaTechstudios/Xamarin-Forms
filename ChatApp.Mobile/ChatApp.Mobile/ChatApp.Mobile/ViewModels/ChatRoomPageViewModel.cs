using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ChatApp.Mobile.ViewModels
{
    public class ChatRoomPageViewModel : ViewModelBase
    {
        private readonly IChatService chatService;

        private string userName;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }


        private IEnumerable<MessageModel> messageList;
        public IEnumerable<MessageModel> MessagesList 
        { 
            get => messageList;
            set => SetProperty(ref messageList, value); 
        }
        public ICommand SendMsgCommand { get; private set; }


        public ChatRoomPageViewModel(
            INavigationService navigationService,
            IChatService chatService,
            ISessionService sessionService)
            : base(navigationService, sessionService)
        {
            this.chatService = chatService;
            SendMsgCommand = new DelegateCommand(SendMsg);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            UserName = parameters.GetValue<string>("UserNameId");
            MessagesList = new List<MessageModel>();
            try
            {
                chatService.ReceiveMessage(GetMessage);
                await chatService.Connect(UserName);
            }
            catch (System.Exception exp)
            {
                throw;
            }

        }

        private void SendMsg()
        {
            chatService.SendMessage(UserName, Message);
            AddMessage(UserName, Message, true);
        }

        private void GetMessage(string userName, string message)
        {
            AddMessage(userName, message, false);
        }


        private void AddMessage(string userName, string message, bool isOwner)
        {
            var tempList = MessagesList.ToList();
            tempList.Add(new MessageModel { IsOwnerMessage = isOwner, Message = message, UseName = userName });
            MessagesList = new List<MessageModel>(tempList);
            Message = string.Empty;
        }

     
    }
}
