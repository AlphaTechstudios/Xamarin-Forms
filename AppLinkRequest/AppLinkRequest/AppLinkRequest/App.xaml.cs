using AppLinkRequest.ViewModels;
using AppLinkRequest.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using System;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace AppLinkRequest
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);

            if (uri.Host.ToLower() == "yourdomain" && uri.Segments != null && uri.Segments.Length == 3)
            {
                string action = uri.Segments[1].Replace("/", "");
                bool isActionParamsValid = long.TryParse(uri.Segments[2], out long productId);
                if(action.ToLower() == "productdetails" && isActionParamsValid )
                {
                    if(productId > 0)
                    {
                        // Navigate to you product details page.
                        NavigationService.NavigateAsync("ProductDetailsPage", new NavigationParameters { { "productId", productId } });
                    }
                    else
                    {
                        // it can be security attack => navigate to home page or login page.
                        NavigationService.NavigateAsync("MainPage");
                    }
                }
            }


        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailsPage, ProductDetailsViewModel>();
        }
    }
}
