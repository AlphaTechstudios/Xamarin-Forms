namespace StripeGateway.API.Models
{
    public class PaymentModel
    {
        /// <summary>
        /// Gets or sets the payment token from client.
        /// </summary>
        public string Token { get; set; }

        public long Amount { get; set; }

        public string Description { get; set; }

    }
}
