using Microsoft.AspNetCore.Mvc;
using StripeGateway.API.Models;
using StripeGateway.API.Services;

namespace StripeGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost("PayWithCard")]
        public IActionResult PayWithCard([FromBody] PaymentModel paymentModel)
        {
            var success = paymentService.PayWithCard(paymentModel);
            return Ok(success);
        }
    }
}
