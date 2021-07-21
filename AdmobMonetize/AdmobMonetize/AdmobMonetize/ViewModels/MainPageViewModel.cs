using MarcTron.Plugin;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace AdmobMonetize.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand ShowRewardedVideoCommand { get; private set; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            ShowRewardedVideoCommand = new DelegateCommand(ShowRewardedVideo);
            CrossMTAdmob.Current.OnRewardedVideoAdLoaded += Current_OnRewardedVideoAdLoaded;
            CrossMTAdmob.Current.OnRewardedVideoAdCompleted += Current_OnRewardedVideoAdCompleted;
            CrossMTAdmob.Current.OnRewardedVideoAdClosed += Current_OnRewardedVideoAdClosed;
        }

        private void Current_OnRewardedVideoAdClosed(object sender, EventArgs e)
        {
            Debug.WriteLine(e);

        }

        private void Current_OnRewardedVideoAdCompleted(object sender, EventArgs e)
        {
            Debug.WriteLine(e);
        }

        private void Current_OnRewardedVideoAdLoaded(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.ShowRewardedVideo();
        }

        private void ShowRewardedVideo()
        {
            CrossMTAdmob.Current.LoadRewardedVideo("ca-app-pub-3940256099942544/5224354917", new MTRewardedAdOptions { CustomData = "1€", UserId = "5" });
        }
    }
}
