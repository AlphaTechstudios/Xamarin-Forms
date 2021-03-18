using ChatApp.Mobile.Services.Core;
using ChatApp.Mobile.Services.Interfaces;
using ChatApp.Mobile.ViewModels;
using ChatApp.Mobile.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ChatApp.Mobile
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterForNavigation<ChatRoomPage, ChatRoomPageViewModel>();
            containerRegistry.RegisterForNavigation<FriendsPage, FriendsPageViewModel>();

            containerRegistry.Register<IChatService, ChatService>();
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.Register<ISessionService, SessionService>();
            containerRegistry.Register<IUsersService, UsersService>();

            containerRegistry.RegisterForNavigation<PrivateChatPage, PrivateChatPageViewModel>();
        }
    }
}
