using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Webkit;
using ChatApp.Mobile.Droid;
using ChatApp.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(GenericWebView), typeof(AndroidWebViewRenderer))]
namespace ChatApp.Mobile.Droid
{
    public class AndroidWebViewRenderer : WebViewRenderer
    {
        Activity mContext;
        public AndroidWebViewRenderer(Context context) : base(context)
        {
            this.mContext = context as Activity;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            Control.Settings.JavaScriptEnabled = true;
            Control.ClearCache(true);
            Control.Settings.MediaPlaybackRequiresUserGesture = false;
            Control.SetWebChromeClient(new MyWebClient(mContext));
        }
        public class MyWebClient : WebChromeClient
        {
            Activity mContext;
            public MyWebClient(Activity context)
            {
                this.mContext = context;
            }
            [TargetApi(Value = 21)]
            public override void OnPermissionRequest(PermissionRequest request)
            {
                mContext.RunOnUiThread(() =>
                {
                    request.Grant(request.GetResources());
                });
            }
        }
    }
}