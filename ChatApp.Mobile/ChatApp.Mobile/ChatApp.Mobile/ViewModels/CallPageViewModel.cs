using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Navigation;

namespace ChatApp.Mobile.ViewModels
{
    public class CallPageViewModel : ViewModelBase
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

        private bool isAudioActive;
        public bool IsAudioActive
        {
            get => isAudioActive;
            set => SetProperty(ref isAudioActive, value);
        }

        private bool isVideoActive;
        public bool IsVideoActive
        {
            get => isVideoActive;
            set => SetProperty(ref isVideoActive, value);
        }

        public CallPageViewModel(INavigationService navigationService,
            ISessionService sessionService)
            : base(navigationService, sessionService)
        {
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            CurrentUser = await SessionService.GetConnectedUser();
            Friend = JsonConvert.DeserializeObject<UserModel>(parameters.GetValue<string>("friendObject"));
        }

    }
}
