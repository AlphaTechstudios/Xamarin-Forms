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
    public class IncomeCallPageViewModel : ViewModelBase
    {
        private readonly IChatService chatService;
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

        public ICommand AcceptCallCommand { get; private set; }
        public ICommand RejectCallCommand { get; private set; }

        public IncomeCallPageViewModel(INavigationService navigationService,
            ISessionService sessionService,
             IChatService chatService)
            : base(navigationService, sessionService)
        {
            AcceptCallCommand = new DelegateCommand(AcceptCall);
            RejectCallCommand = new DelegateCommand(RejectCall);
            this.chatService = chatService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            CurrentUser = await SessionService.GetConnectedUser();
            Friend = JsonConvert.DeserializeObject<UserModel>(parameters.GetValue<string>("friendObject"));
            try
            {
                await chatService.Connect(currentUser.Email);
            }
            catch (System.Exception exp)
            {
                throw;
            }
        }

        private async void RejectCall()
        {
            await chatService.RejectVideoCall(CurrentUser.Email, Friend.Email);
            await NavigationService.NavigateAsync($"../{nameof(FriendsPage)}");
        }

        private async void AcceptCall()
        {
            await chatService.AcceptVideoCall(CurrentUser.Email, Friend.Email);
            await NavigationService.NavigateAsync($"../{nameof(CallPage)}", new NavigationParameters { { "friendObject", JsonConvert.SerializeObject(Friend) } });
        }
    }
}
