using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using StripePayment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace StripePayment.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IStripePaymentService stripePaymentService;

        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string Cvv { get; set; }
        public string Amount { get; set; }

        public ICommand PayCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService,
            IStripePaymentService stripePaymentService)
            : base(navigationService)
        {
            Title = "Main Page";
            PayCommand = new DelegateCommand(Pay);
            this.stripePaymentService = stripePaymentService;
        }

        private async void Pay()
        {
            var token = stripePaymentService.GeneratePaymentToken(new Models.CardModel
            {
                Number = CardNumber.Replace(" ", string.Empty),
                ExpMonth = Convert.ToInt16( Expiry.Substring(0,2)),
                ExpYear = Convert.ToInt16(Expiry.Substring(3, 2)),
                Cvc = Cvv,
                Name = "Brice Devos",
                AddressCity = "Paris",
                AddressZip = "75008",
                AddressCountry = "France",
                AddressLine1 = "16 Rue Victor Hugo"
            });
            var success = await stripePaymentService.PayWithCard(new Models.PaymentModel { 
                Amount = Convert.ToInt16(Amount) * 100,
                Token = token,
                Description = "Stripe test payment subscription"
            });

        }
    }
}
