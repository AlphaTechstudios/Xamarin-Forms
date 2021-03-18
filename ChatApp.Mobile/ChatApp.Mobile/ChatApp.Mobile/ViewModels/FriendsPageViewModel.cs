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
    public class FriendsPageViewModel : ViewModelBase
    {
        private IEnumerable<UserModel> friendsList;
        private readonly IUsersService usersService;

        public IEnumerable<UserModel> FriendsList
        {
            get => friendsList;
            set =>SetProperty(ref friendsList, value);
        }

        public ICommand GoToPrivateDiscussionCommand { get; private set; }
        public FriendsPageViewModel(INavigationService navigationService,
            ISessionService sessionService,
            IUsersService usersService)
            : base(navigationService, sessionService)
        {
            this.usersService = usersService;

            GoToPrivateDiscussionCommand = new DelegateCommand<UserModel>(GoToPrivateDiscussion);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            var currentUser = await SessionService.GetConnectedUser();
            FriendsList = await usersService.GetUserFriendsAsync(currentUser.ID);
        }

        private async void GoToPrivateDiscussion(UserModel friend)
        {
            var param = new NavigationParameters { { "friend", JsonConvert.SerializeObject(friend) } };
            await NavigationService.NavigateAsync("PrivateChatPage", param);
        }
    }
}
