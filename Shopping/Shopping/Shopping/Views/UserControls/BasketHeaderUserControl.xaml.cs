using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shopping.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketHeaderUserControl : Grid
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(BasketHeaderUserControl));

        public static readonly BindableProperty ProductCountProperty =
          BindableProperty.Create("ProductCount", typeof(int), typeof(BasketHeaderUserControl));


        public static readonly BindableProperty GoToBasketCommandProperty =
            BindableProperty.Create("GoToBasketCommand", typeof(ICommand), typeof(BasketHeaderUserControl));

        public int ProductCount
        {
            get => (int)GetValue(ProductCountProperty);
            set => SetValue(ProductCountProperty, value);
        }


        public string Title
        {
            get => (string)GetValue(TitleProperty) ;
            set => SetValue(TitleProperty, value);
        }

        public ICommand GoToBasketCommand
        {
            get => (ICommand)GetValue(GoToBasketCommandProperty);
            set => SetValue(GoToBasketCommandProperty, value);
        }

        public BasketHeaderUserControl()
        {
            InitializeComponent();
        }
    }
}