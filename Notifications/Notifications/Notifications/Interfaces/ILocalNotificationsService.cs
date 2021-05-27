using System.Collections.Generic;

namespace Notifications.Interfaces
{
    public interface ILocalNotificationsService
    {
        void ShowNotification(string title, string message, IDictionary<string, string> data);
    }
}
