using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Notifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notifications.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class CustomFirebaseMessagingService : FirebaseMessagingService
    {
        public readonly ILocalNotificationsService localNotificationsService;

        public CustomFirebaseMessagingService()
        {
            localNotificationsService = new LocalNotificationsService();
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            Log.Debug("FMC_SERVICE", token);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            var notification = message.GetNotification();
            localNotificationsService.ShowNotification(notification.Title, notification.Body, message.Data);
        }

    }
}