using Newtonsoft.Json;
using Stripe;
using StripePayment.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StripePayment.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        string apiUri = "http://192.168.1.36:45455/api/Payments/";
        public async Task<bool> PayWithCard(PaymentModel paymentModel)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(apiUri)})
            {
                try
                {
                    var content = JsonConvert.SerializeObject(paymentModel);
                    HttpContent postContent = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("PayWithCard", postContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return default;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception exp)
                {
                    throw;
                }
            }
        }
   
        public string GeneratePaymentToken(CardModel cardModel)
        {
            StripeConfiguration.ApiKey = "pk_test_51IhYz8GfByAJflJA3YrBI0qUpdWaP6tiGLGZdATIFVWtVWGPbx4Xw1N2YfQONVkE4yYFvMji5DKsV9uAOMbEUau200cWhg743g";
            var option = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = cardModel.Number,
                    ExpMonth = cardModel.ExpMonth,
                    ExpYear = cardModel.ExpYear,
                    Cvc = cardModel.Cvc,
                    Currency = "EUR",
                    Name = cardModel.Name,
                    AddressCity = cardModel.AddressCity,
                    AddressZip = cardModel.AddressZip,
                    AddressLine1 = cardModel.AddressLine1,
                    AddressCountry = cardModel.AddressCountry
                }
            };

            var service = new TokenService();
            var token = service.Create(option);
            return token.Id;
        }
    }
}
