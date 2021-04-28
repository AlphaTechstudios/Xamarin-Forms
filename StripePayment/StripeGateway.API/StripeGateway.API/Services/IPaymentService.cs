using StripeGateway.API.Models;

namespace StripeGateway.API.Services
{
    public interface IPaymentService
    {
        bool PayWithCard(PaymentModel paymentModel); 
    }
}
