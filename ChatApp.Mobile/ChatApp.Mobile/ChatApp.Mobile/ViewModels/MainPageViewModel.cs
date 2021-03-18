using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using ChatApp.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Windows.Input;

namespace ChatApp.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string pwd;
        private readonly IAuthenticationService authenticationService;

        public string Pwd
        {
            get => pwd;
            set => SetProperty(ref pwd, value);
        }

        public ICommand SignInCommand { get; private set; }

        public ICommand NavigateToChatPageCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService,
             IAuthenticationService authenticationService,
             ISessionService sessionService)
            : base(navigationService, sessionService)
        {
            NavigateToChatPageCommand = new DelegateCommand(NavigateToChatPage);
            SignInCommand = new DelegateCommand(SignIn);
            this.authenticationService = authenticationService;
        }

        private async void SignIn()
        {
            var user = await authenticationService.Login(new LoginModel { Email = Email, Password = Pwd });
            if (user != null)
            {
                await SessionService.SetConnectedUser(user);
                await SessionService.SetToken(new TokenModel
                {
                    Token = user.Token,
                    RefreshToken = user.RefreshToken,
                    TokenExpireTime = user.TokenExpireTimes
                });

                await NavigationService.NavigateAsync("../FriendsPage");
            }
            else
            {
                // TODO Show login error.
            }
        }

        private void NavigateToChatPage()
        {
            var param = new NavigationParameters { { "UserNameId", Email } };
            NavigationService.NavigateAsync($"NavigationPage/{nameof(ChatRoomPage)}", param);
        }
    }
}
