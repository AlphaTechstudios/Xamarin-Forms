using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppLinkRequest.ViewModels
{
    public class ProductDetailsViewModel : ViewModelBase
    {
        private long productId;
        public long ProductId 
        {
            get => productId;
            set => SetProperty(ref productId, value);
        }

        private string productImageUrl;
        public string ProductImageUrl
        {
            get => productImageUrl;
            set => SetProperty(ref productImageUrl, value);
        }

        public ProductDetailsViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if(parameters.ContainsKey("productId"))
            {
                ProductId = parameters.GetValue<long>("productId");

                if(ProductId == 1)
                {
                    ProductImageUrl = "https://images.asos-media.com/products/public-desire-cowl-midi-dress-with-strap-detail-in-white/24108618-2";
                }
                else
                {
                    ProductImageUrl = "https://images.asos-media.com/products/asos-design-ruched-cut-out-mini-dress-with-fluted-long-sleeves-in-red/21877921-3";
                }
            }

        }
    }
}
