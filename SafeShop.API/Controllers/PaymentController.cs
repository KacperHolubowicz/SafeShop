using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafeShop.API.Responses;
using SafeShop.Service.DTO.Payment;
using Stripe;
using Stripe.Checkout;

namespace SafeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly string stripeKey;
        private readonly string successUrl;
        private readonly string cancelUrl;

        public PaymentController(IConfiguration configuration)
        {
            stripeKey = configuration["StripeTestApiKey"];
            successUrl = configuration["SuccessUrl"];
            cancelUrl = configuration["CancelUrl"];
        }

        [HttpPost]
        public async Task<ActionResult<PaymentResponse>> CreateSession([FromBody] PaymentPostDTO payment)
        {
            StripeConfiguration.ApiKey = stripeKey;
            var options = new SessionCreateOptions
            {
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                LineItems = payment.Items.Select(i => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "pln",
                        UnitAmountDecimal = i.Price * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions() 
                        {
                            Name = i.Name
                        },
                    },
                    Quantity = i.Quantity
                }).ToList(),
                Mode = "payment",
                PaymentMethodTypes = new List<string>
                {
                    "card"
                }
            };
            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return Ok(new PaymentResponse() { Url = session.Url });
        }
    }
}
