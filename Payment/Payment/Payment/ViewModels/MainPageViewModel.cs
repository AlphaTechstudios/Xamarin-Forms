using Payment.Models;
using Payment.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Payment.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IPaymentsService paymentsService;

        private CreditCardModel creditCard;
        public CreditCardModel CreditCard 
        { 
            get => creditCard;
            set => SetProperty(ref creditCard, value); 
        }

        public ICommand PayCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPaymentsService paymentsService)
            : base(navigationService)
        {
            Title = "Credit Card Payment";
            this.paymentsService = paymentsService;
            PayCommand = new DelegateCommand(async () => await CreatePayment());
            CreditCard = new CreditCardModel();
        }

        private async Task CreatePayment()
        {
            var date = CreditCard.Expiry.Split('/');

            await paymentsService.PayWithCreditCard(CreditCard.CardNumber, date[0], date[1], CreditCard.Cvv);
        }
    }
}
