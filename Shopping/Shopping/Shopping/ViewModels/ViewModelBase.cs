using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Shopping.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shopping.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int productCount;
        public int ProductCount
        {
            get { return productCount; }
            set { SetProperty(ref productCount, value); }
        }

        public ICommand GoToBasketCommand { get; private set; }
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            GoToBasketCommand = new DelegateCommand(GoToBasket);
            SubscribeToEvents();
        }

        private void GoToBasket()
        {
            NavigationService.NavigateAsync("NavigationPage/BasketPage");
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
           
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
         
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            var productsList = GetBasketProductsList();
            ProductCount = productsList.Count();
        }

        public virtual void Destroy()
        {

        }

        private void SubscribeToEvents()
        {
            MessagingCenter.Unsubscribe<string, ProductModel>("UpdateBasket", "Add product");
            MessagingCenter.Subscribe<string, ProductModel>("UpdateBasket", "Add product", (sender, args) => {
              
                List<ProductModel> productsList = new List<ProductModel>();
                var jsonProductsList = Preferences.Get("BasketList", null);
                if (!string.IsNullOrEmpty(jsonProductsList))
                {
                    productsList = JsonConvert.DeserializeObject<List<ProductModel>>(Preferences.Get("BasketList", null));
                }
                productsList.Add(args);
                ProductCount = productsList.Count;
                Preferences.Set("BasketList", JsonConvert.SerializeObject(productsList));
            });
        }

        protected IEnumerable<ProductModel> GetBasketProductsList()
        {
            var jsonProductsList = Preferences.Get("BasketList", null);
            if (!string.IsNullOrEmpty(jsonProductsList))
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(jsonProductsList);
            }
            return new List<ProductModel>();
        }

    }
}
