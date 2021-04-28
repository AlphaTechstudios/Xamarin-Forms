using StripePayment.Models;
using System.Threading.Tasks;

namespace StripePayment.Services
{
    public interface IStripePaymentService
    {
        Task<bool> PayWithCard(PaymentModel paymentModel);
        string GeneratePaymentToken(CardModel cardModel);
    }
}
