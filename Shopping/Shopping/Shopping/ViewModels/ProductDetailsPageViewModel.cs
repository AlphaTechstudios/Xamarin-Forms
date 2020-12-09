using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Shopping.Models.Models;
using Shopping.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.ViewModels
{
    public class ProductDetailsPageViewModel : ViewModelBase
    {
        #region Properties
        private IEnumerable<string> productImagesList;
        private readonly IProductsService productsService;

        public IEnumerable<string> ProductImagesList
        {
            get => productImagesList;
            set => SetProperty(ref productImagesList, value);
        }

        private string productName;
        public string ProductName 
        { 
            get => productName;
            set => SetProperty(ref productName, value);
        }

        private float price;
        public float Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        #endregion
        #region Commands
        #endregion
        public ProductDetailsPageViewModel(INavigationService navigationService, IProductsService productsService)
            : base(navigationService)
        {
            this.productsService = productsService;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            ProductModel productModel = JsonConvert.DeserializeObject<ProductModel>(parameters.GetValue<string>("Product"));
            Title = productModel.Name;
            ProductImagesList = productsService.GetProductsImages(productModel.ID);
            ProductName = productModel.Name;
            Price = productModel.Price;

        }
    }
}
