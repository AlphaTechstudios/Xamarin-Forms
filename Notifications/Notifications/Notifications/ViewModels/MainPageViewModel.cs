using Notifications.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notifications.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public readonly ILocalNotificationsService localNotificationsService;

        public ICommand ShowNotificationCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Notifications";

            localNotificationsService = DependencyService.Get<ILocalNotificationsService>();
            ShowNotificationCommand = new DelegateCommand(ShowNotification);
        }

        private void ShowNotification()
        {
            localNotificationsService.ShowNotification("Local Notification", "This a local notification", new Dictionary<string, string>());
        }
    }
}
