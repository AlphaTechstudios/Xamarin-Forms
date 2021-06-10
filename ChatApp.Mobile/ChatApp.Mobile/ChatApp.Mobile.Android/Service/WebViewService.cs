using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChatApp.Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(ChatApp.Mobile.Droid.Service.WebViewService))]
namespace ChatApp.Mobile.Droid.Service
{
    public class WebViewService : IWebViewService
    {
        public string GetContent()
        {
            return "file:///android_asset/call.html";
        }
    }
}