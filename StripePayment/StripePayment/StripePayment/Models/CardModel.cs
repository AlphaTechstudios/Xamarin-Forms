namespace StripePayment.Models
{
    public class CardModel
    {
        public string Number { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public string Cvc { get; set; }
        public string Name { get; set; }
        public string AddressCity { get; set; }
        public string AddressZip { get; set; }
        public string Currency { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressCountry { get; set; }

    }
}
