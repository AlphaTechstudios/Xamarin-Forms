using CollectionView.Models;
using CollectionView.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CollectionView.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IUsersService usersService;


        private IEnumerable<UserModel> users;

        public IEnumerable<UserModel> Users
        {
            get => users;
            set => SetProperty(ref users, value);
        }

        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }


        public MainPageViewModel(INavigationService navigationService, IUsersService usersService)
            : base(navigationService)
        {
            Title = "Users List";
            this.usersService = usersService;

            EditCommand = new DelegateCommand<UserModel>(EditUser);
            DeleteCommand = new DelegateCommand<UserModel>(DeleteUser);
        }

        private async void DeleteUser(UserModel userModel)
        {
            await App.Current.MainPage.DisplayAlert("Alert", "This is a delete action!", "Ok");
        }

        private async  void EditUser(UserModel userModel)
        {
            await App.Current.MainPage.DisplayAlert("Alert", "This is an edit action!", "Ok");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            InitData();
        }

        private async void InitData()
        {
            Users = await usersService.GetUsers();
        }
    }
}
