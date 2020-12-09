using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Services
{
    public interface IPaymentsService
    {
        Task PayWithCreditCard(string panNumber, string expirationMonth, string expirationYear, string cvv = null);
        //event EventHandler<string> OnTokenizationSuccessful;

        //event EventHandler<string> OnTokenizationError;

        //bool CanPay { get; }

        //Task<bool> InitializeAsync(string clientToken);

        //Task<string> TokenizeCard(string panNumber = "4111111111111111", string expirationMonth = "12", string expirationYear = "2018", string cvv = null);

    }
}
