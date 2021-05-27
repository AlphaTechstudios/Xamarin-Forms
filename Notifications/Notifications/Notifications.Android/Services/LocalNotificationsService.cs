using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using Notifications.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;
[assembly:Dependency(typeof(Notifications.Droid.Services.LocalNotificationsService))]
namespace Notifications.Droid.Services
{
    public class LocalNotificationsService: ILocalNotificationsService
    {

        private const string CHANNEL_ID = "local_notifications_channel";
        private const string CHANNEL_NAME = "Notifications";
        private const string CHANNEL_DESCRIPTION = "Local and push notifications messages appear in this channel";

        private int notificationId = -1;
        private const string TITLE_KEY = "title";
        private const string MESSAGE_KEY = "message";

        private bool isChannelInitialized = false;
                
        private void CreateNotificationChannel()
        {
            if(Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID, CHANNEL_NAME, NotificationImportance.Default)
            {
                Description = CHANNEL_DESCRIPTION
            };

            var notificationManager = (NotificationManager) AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public void ShowNotification(string title, string message, IDictionary<string, string> data)
        {
            if (!isChannelInitialized)
            {
                CreateNotificationChannel();
            }

            var intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TITLE_KEY, title);
            intent.PutExtra(MESSAGE_KEY, message);
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            notificationId++;

            var pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, notificationId, intent , PendingIntentFlags.OneShot);
            var notificationBuilder = new NotificationCompat.Builder(AndroidApp.Context, CHANNEL_ID)
                                            .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Mipmap.icon))
                                            .SetSmallIcon(Resource.Mipmap.icon)
                                            .SetContentTitle(title)
                                            .SetContentText(message)
                                            .SetAutoCancel(true)
                                            .SetContentIntent(pendingIntent)
                                            .SetDefaults((int)NotificationDefaults.Sound |(int)NotificationDefaults.Vibrate);

            var notificationManager = NotificationManagerCompat.From(AndroidApp.Context);
            notificationManager.Notify(notificationId, notificationBuilder.Build());
        }
    }
}