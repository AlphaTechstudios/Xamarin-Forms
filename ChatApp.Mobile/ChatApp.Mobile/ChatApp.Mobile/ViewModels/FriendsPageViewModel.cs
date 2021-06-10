using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using ChatApp.Mobile.Views;
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
    public class FriendsPageViewModel : ViewModelBase
    {
        private IEnumerable<UserModel> friendsList;
        private readonly IUsersService usersService;
        private readonly IChatService chatService;

        public IEnumerable<UserModel> FriendsList
        {
            get => friendsList;
            set => SetProperty(ref friendsList, value);
        }

        public ICommand GoToPrivateDiscussionCommand { get; private set; }
        public ICommand VideoCallCommand { get; private set; }
        
        public FriendsPageViewModel(INavigationService navigationService,
            ISessionService sessionService,
            IUsersService usersService,
             IChatService chatService)
            : base(navigationService, sessionService)
        {
            this.usersService = usersService;
            this.chatService = chatService;
            GoToPrivateDiscussionCommand = new DelegateCommand<UserModel>(GoToPrivateDiscussion);
            VideoCallCommand = new DelegateCommand<UserModel>(VideoCall);
        }

        private async void VideoCall(UserModel friend)
        {
            await chatService.CallFriend(friend.Email);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            var currentUser = await SessionService.GetConnectedUser();
            FriendsList = await usersService.GetUserFriendsAsync(currentUser.ID);
            try
            {
                chatService.ReceivePrivateVideoCall(GetVideoCall);
                chatService.AcceptVideoCallByFriend(AcceptVideoCallByFriend);
                await chatService.Connect(currentUser.Email);
            }
            catch (System.Exception exp)
            {
                throw;
            }
        }

        private void AcceptVideoCallByFriend(string currentUser, string friendEmail)
        {
            NavigationService.NavigateAsync($"../{nameof(CallPage)}", new NavigationParameters { { "friendObject", JsonConvert.SerializeObject(FriendsList.SingleOrDefault(x => x.Email == currentUser)) } });

        }

        private void GetVideoCall(string from)
        {
            NavigationService.NavigateAsync($"../{nameof(IncomeCallPage)}", new NavigationParameters { {"friendObject", JsonConvert.SerializeObject(FriendsList.SingleOrDefault(x=>x.Email == from)) } });
        }

        private async void GoToPrivateDiscussion(UserModel friend)
        {
            var param = new NavigationParameters { { "friend", JsonConvert.SerializeObject(friend) } };
            await NavigationService.NavigateAsync("PrivateChatPage", param);
        }
    }
}
