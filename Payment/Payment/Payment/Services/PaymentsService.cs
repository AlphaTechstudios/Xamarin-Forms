using Braintree;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Services
{
    public class PaymentsService : IPaymentsService
    {
        private BraintreeGateway GetBrainTreeGateway()
        {
            return new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "mp5fv6bwzjg2b7w5",
                PublicKey = "d75pywfcwh5m8z4s",
                PrivateKey = "216c3d258eac098b96544199925651ce"
            };
        }

        public async Task PayWithCreditCard(string panNumber, string expirationMonth, string expirationYear, string cvv = null)
        {
            var gateWay = GetBrainTreeGateway();
            var creditCardRequest = new TransactionCreditCardRequest
            {
                Number = panNumber,
                ExpirationMonth = expirationMonth,
                ExpirationYear = expirationYear,
                CVV = cvv
            };

            TransactionRequest request = new TransactionRequest
            {
                Amount = 1000.00M,
                PaymentMethodNonce = "fake-valid-nonce",
                CreditCard = creditCardRequest,

                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };
            
            Result<Transaction> result = await gateWay.Transaction.SaleAsync(request);

            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                Console.WriteLine("Success!: " + transaction.Id);
            }
            else if (result.Transaction != null)
            {
                Transaction transaction = result.Transaction;
                Console.WriteLine("Error processing transaction:");
                Console.WriteLine("  Status: " + transaction.Status);
                Console.WriteLine("  Code: " + transaction.ProcessorResponseCode);
                Console.WriteLine("  Text: " + transaction.ProcessorResponseText);
            }
            else
            {
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    Console.WriteLine("Attribute: " + error.Attribute);
                    Console.WriteLine("  Code: " + error.Code);
                    Console.WriteLine("  Message: " + error.Message);
                }
            }

        }
    }
}
