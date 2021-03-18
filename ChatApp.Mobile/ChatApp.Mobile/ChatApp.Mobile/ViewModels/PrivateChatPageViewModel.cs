using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ChatApp.Mobile.ViewModels
{
    public class PrivateChatPageViewModel : ViewModelBase
    {
        private UserModel friend;
        public UserModel Friend
        {
            get => friend;
            set => SetProperty(ref friend, value);
        }

        private UserModel currentUser;
        public UserModel CurrentUser
        {
            get => currentUser;
            set => SetProperty(ref currentUser, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }


        private IEnumerable<MessageModel> messageList;
        private readonly IChatService chatService;

        public IEnumerable<MessageModel> MessagesList
        {
            get => messageList;
            set => SetProperty(ref messageList, value);
        }
        public ICommand SendMsgCommand { get; private set; }

        public PrivateChatPageViewModel(INavigationService navigationService,
            ISessionService sessionService,
            IChatService chatService)
            : base(navigationService, sessionService)
        {
            SendMsgCommand = new DelegateCommand(SendMsg);
            this.chatService = chatService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            var friendString = parameters.GetValue<string>("friend");
            Friend = JsonConvert.DeserializeObject<UserModel>(friendString);
            Title = friend.Name;
            CurrentUser = await SessionService.GetConnectedUser();
            MessagesList = new List<MessageModel>();
            try
            {
                chatService.ReceiveMessage(GetMessage, true);
                await chatService.Connect(CurrentUser.Email);
            }
            catch (System.Exception exp)
            {
                throw;
            }
        }

        public override async void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            await chatService.Disconnect(CurrentUser.Email);
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

        private void SendMsg()
        {
            chatService.SendMessage(friend.Email, Message, true);
            AddMessage(friend.Name, Message, true);
        }
    }
}
