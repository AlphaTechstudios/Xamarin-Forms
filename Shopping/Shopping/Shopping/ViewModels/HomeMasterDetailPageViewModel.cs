using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Shopping.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shopping.ViewModels
{
    public class HomeMasterDetailPageViewModel : ViewModelBase
    {
        #region Properties
        #endregion

        #region Commands
        public ICommand MenuItemClickCommand { get; private set; }
        #endregion
        public HomeMasterDetailPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            MenuItemClickCommand = new DelegateCommand<string>(MenuItemClick);
        }

        private void MenuItemClick(string categoryName)
        {
            var nagivationParams = new NavigationParameters { { "CategoryName", categoryName } };
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ProductsPage)}", nagivationParams);
        }
    }
}
