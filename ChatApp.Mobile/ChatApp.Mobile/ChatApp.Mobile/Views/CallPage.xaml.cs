using ChatApp.Mobile.Services.Interfaces;
using ChatApp.Mobile.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChatApp.Mobile.Views
{
    public partial class CallPage : ContentPage
    {
        private CallPageViewModel callPageViewModel;
        public CallPage()
        {
            InitializeComponent();

            var urlSource = new UrlWebViewSource();
            string baseUrl = DependencyService.Get<IWebViewService>().GetContent();
            urlSource.Url = baseUrl;
            CallWebView.Source = urlSource;
            callPageViewModel = BindingContext as CallPageViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(1000);

            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                var response = await Permissions.RequestAsync<Permissions.Camera>();
                if (response != PermissionStatus.Granted)
                {
                }
            }

            var statusMic = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (statusMic != PermissionStatus.Granted)
            {
                var response = await Permissions.RequestAsync<Permissions.Microphone>();
                if (response != PermissionStatus.Granted)
                {
                }
            }
            await Connect();
        }

        private async Task Connect()
        {
            try
            {
                await CallWebView.EvaluateJavaScriptAsync($"init('{callPageViewModel.CurrentUser.ID}');");
                await Task.Delay(2000);
                await CallWebView.EvaluateJavaScriptAsync($"startCall('{callPageViewModel.Friend.ID}');");
            }
            catch (Exception exp)
            {

            }
        }

        private async void ToggleMicClick(object sender, EventArgs e)
        {
            callPageViewModel.IsAudioActive = !callPageViewModel.IsAudioActive;
            await CallWebView.EvaluateJavaScriptAsync($"toggleAudio('{callPageViewModel.IsAudioActive.ToString().ToLower()}');");
        }

        private void EndCallClick(object sender, EventArgs e)
        {
            EndCall();
        }

        private async void ToggelCameraClick(object sender, EventArgs e)
        {
            callPageViewModel.IsVideoActive = !callPageViewModel.IsVideoActive;
            await CallWebView.EvaluateJavaScriptAsync($"toggleVideo('{callPageViewModel.IsVideoActive.ToString().ToLower()}');");
        }

        private void EndCall()
        {
            CallWebView.Source = "about:blank";
        }
    }
}
