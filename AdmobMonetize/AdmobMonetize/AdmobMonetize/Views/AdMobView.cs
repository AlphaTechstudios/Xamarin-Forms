using System;
using Xamarin.Forms;

namespace AdmobMonetize.Views
{
    public class AdMobView : View
    {
        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
            nameof(AdUnitId),
            typeof(string),
            typeof(AdMobView),
            string.Empty);

        public static readonly BindableProperty AdTypeProperty = BindableProperty.Create(
            nameof(AdType),
            typeof(string),
            typeof(AdMobView),
            string.Empty);

        public static readonly BindableProperty AdWidthProperty = BindableProperty.Create(
            nameof(AdWidth),
            typeof(int),
            typeof(AdMobView),
            0);

        public string AdUnitId
        {
            get => Convert.ToString(GetValue(AdUnitIdProperty));
            set => SetValue(AdUnitIdProperty, value);
        }


        public string AdType
        {
            get => Convert.ToString(GetValue(AdTypeProperty));
            set => SetValue(AdTypeProperty, value);
        }

        public int AdWidth
        {
            get => Convert.ToInt32(GetValue(AdWidthProperty));
            set => SetValue(AdWidthProperty, value);
        }
    }
}
