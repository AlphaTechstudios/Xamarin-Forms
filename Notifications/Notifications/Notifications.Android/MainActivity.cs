using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Util;
using Prism;
using Prism.Ioc;

namespace Notifications.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            IsGoogleServicesAvailable();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private bool IsGoogleServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if(resultCode != ConnectionResult.Success)
            {
                if(GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Log.Error("MAINACTIVITY", "SDK not installed");

                }
                else
                {
                    Log.Error("MAINACTIVITY", "This device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {
                Log.Debug("MAINACTIVITY", "Google Services are available");
                return true;
            }
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

