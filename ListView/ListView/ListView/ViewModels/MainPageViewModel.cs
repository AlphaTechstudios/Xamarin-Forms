using ListView.Models;
using ListView.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListView.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IProductsService productsService;

        private IEnumerable<ProductModel> products;

        public IEnumerable<ProductModel> Products 
        { 
            get => products;
            set => SetProperty(ref products, value);
        }

        private IEnumerable<ProductsVM> productsList;

        public IEnumerable<ProductsVM> ProductsList
        {
            get => productsList;
            set
            {
                SetProperty(ref productsList, value);
                OnPropertyChanged(new PropertyChangedEventArgs("ProductsList"));

            }
        }

        public ICommand HeaderClickCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService, IProductsService productsService)
            : base(navigationService)
        {
            this.productsService = productsService;
            HeaderClickCommand = new Command<ProductsVM>((item) => ExecuteHeaderClickCommand(item));

        }

        private void ExecuteHeaderClickCommand(ProductsVM item)
        {
            item.Expanded = !item.Expanded;
            OnPropertyChanged(new PropertyChangedEventArgs("ProductsList"));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            InitData();
        }

        private void InitData()
        {
            Products = productsService.GetAll();

            ProductsList = productsService.GetAllGrouppedByCategory();
        }
    }
}
