using AdmobMonetize.Droid.Views;
using AdmobMonetize.Views;
using Android.Content;
using Android.Gms.Ads;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace AdmobMonetize.Droid.Views
{
    public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
    {
        public AdMobViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement!= null && Control == null)
            {
                SetNativeControl(CreateAdView(e.NewElement.AdWidth));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == nameof(AdMobView.AdUnitId))
            {
                Control.AdUnitId = Element.AdUnitId;
            }
        }

        private AdView CreateAdView(int adWidth)
        {
            var adView = new AdView(Context);
            adView.AdUnitId = Element.AdUnitId;
            if(Element.AdType == "MediumRectangle")
            {
                adView.AdSize = AdSize.MediumRectangle;
            }
            else
            {
                adView.AdSize = AdSize.GetPortraitInlineAdaptiveBannerAdSize(Context, adWidth);
            }

            adView.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);
            return adView;
        }
    }
}