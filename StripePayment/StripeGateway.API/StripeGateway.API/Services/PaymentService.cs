using Stripe;
using StripeGateway.API.Models;

namespace StripeGateway.API.Services
{
    public class PaymentService : IPaymentService
    {
        public bool PayWithCard(PaymentModel paymentModel)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = paymentModel.Amount,
                Currency = "eur",
                Source = paymentModel.Token,
                Description = paymentModel.Description
            };
            var service = new ChargeService();
            var response = service.Create(chargeOptions);

            if (response != null && response.Status.ToLower() == "succeeded")
            {
                return true;
            }
            return false;
        }
    }
}
