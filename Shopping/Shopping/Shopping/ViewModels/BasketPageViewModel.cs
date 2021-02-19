using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Shopping.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Shopping.ViewModels
{
    public class BasketPageViewModel : ViewModelBase
    {
        private IEnumerable<ProductModel> productsList;
        public IEnumerable<ProductModel> ProductsList 
        { 
            get => productsList;
            set => SetProperty(ref productsList, value);
        }

        private float totalPrice;
        public float TotalPrice 
        { 
            get => totalPrice;
            set => SetProperty(ref totalPrice, value); 
        }

        public ICommand DeleteCommand { get; private set; }
        public BasketPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            DeleteCommand = new DelegateCommand<ProductModel>(DeleteProduct);
        }

        private void DeleteProduct(ProductModel productModel)
        {
            var tempList = ProductsList.ToList();
            tempList.Remove(productModel);
            ProductsList = tempList;
            Preferences.Set("BasketList", JsonConvert.SerializeObject(ProductsList));
            InitData();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            InitData();
        }

        private void InitData()
        {
            ProductsList = GetBasketProductsList();
            TotalPrice = ProductsList.Select(x => x.Price).Sum();
        }
    }
}
