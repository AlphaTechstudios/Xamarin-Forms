using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Shopping.Models.Models;
using Shopping.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shopping.ViewModels
{
    public class ProductDetailsPageViewModel : ViewModelBase
    {
        #region Properties
        private readonly IProductsService productsService;

        private ProductModel productModel;
        private IEnumerable<string> productImagesList;

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

        private int quantity;
        public int Quantity 
        { 
            get => quantity;
            set=> SetProperty(ref quantity, value);
        }

        private IEnumerable<string> sizeList;
        public IEnumerable<string> SizeList 
        { 
            get => sizeList;
            set => SetProperty(ref sizeList, value);
        }

        private string selectedSize;
        public string SelectedSize
        {
            get => selectedSize;
            set => SetProperty(ref selectedSize, value);
        }

        #endregion
        #region Commands

        public ICommand IncreaseQuantityCommand { get; set; }
        public ICommand DecreaseQuantityCommand { get; set; }
        public ICommand AddProductToBasketCommand { get; set; }

        

        #endregion
        public ProductDetailsPageViewModel(INavigationService navigationService, IProductsService productsService)
            : base(navigationService)
        {
            this.productsService = productsService;
            IncreaseQuantityCommand = new DelegateCommand(IncreaseQuantity);
            DecreaseQuantityCommand = new DelegateCommand(DecreaseQuantity);
            AddProductToBasketCommand = new DelegateCommand(AddProductToBasket);
        }

        private void AddProductToBasket()
        {
            MessagingCenter.Send("UpdateBasket", "Add product", productModel);
        }

        private void DecreaseQuantity()
        {
            if(Quantity >= 2)
            {
                Quantity--;
            }
        }

        private void IncreaseQuantity()
        {
            Quantity++;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            productModel = JsonConvert.DeserializeObject<ProductModel>(parameters.GetValue<string>("Product"));
            Title = productModel.Name;
            ProductImagesList = productsService.GetProductsImages(productModel.ID);
            ProductName = productModel.Name;
            Price = productModel.Price;

            // Fake data 
            SizeList = new List<string>() { "S", "M", "L" };
            Quantity = 1;

        }
    }
}
