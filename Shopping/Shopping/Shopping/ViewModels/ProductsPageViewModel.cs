using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Shopping.Models.Models;
using Shopping.Services.Interfaces;
using Shopping.Views;
using System.Collections.Generic;
using System.Windows.Input;

namespace Shopping.ViewModels
{
    public class ProductsPageViewModel : ViewModelBase
    {
        #region Properties
        private readonly IProductsService productsService;

        private IEnumerable<ProductModel> productsList;

        public IEnumerable<ProductModel> ProductsList
        {
            get => productsList;
            set => SetProperty(ref productsList, value);
        }

        private ProductModel selecetdProduct;
        public ProductModel SelectedProduct 
        {
            get => selecetdProduct;
            set => SetProperty(ref selecetdProduct, value);
        }
        #endregion

        #region Commands
        public ICommand ShowProductDetailsCommand { get; private set; }
        #endregion

        public ProductsPageViewModel(INavigationService navigationService, IProductsService productsService)
            : base(navigationService)
        {
            this.productsService = productsService;
            ShowProductDetailsCommand = new DelegateCommand<ProductModel>(ShowProductDetails);
        }

        private void ShowProductDetails(ProductModel product)
        {
            var nagivationParams = new NavigationParameters { { "Product", JsonConvert.SerializeObject(product) } };
            NavigationService.NavigateAsync(nameof(ProductDetailsPage), nagivationParams);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
         
        }
        public override void Initialize(INavigationParameters parameters)
        {
            string categoryName = parameters.GetValue<string>("CategoryName");
            Title = categoryName;
            InitData(categoryName);
        }

        private void InitData(string categoryName)
        {
            ProductsList = productsService.GetProductsByGategoryName(categoryName);
        }
    }
}
