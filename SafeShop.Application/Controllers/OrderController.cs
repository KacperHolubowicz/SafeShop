using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShop.Application.Requests;
using SafeShop.Application.Requests.Order;
using SafeShop.Application.Responses;
using SafeShop.Application.ViewModels;

namespace SafeShop.Application.Controllers
{
    public class OrderController : Controller
    {
        //TODO some fallback page or whatever for 1. no cartId in cookies 2. null cart
        private readonly IHttpClientFactory factory;

        public OrderController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderDataViewModel order)
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            string url = await GetPaymentUrl(httpClient);
            await CreateOrder(httpClient, order);
            return Redirect(url);
        }

        public async Task<IActionResult> Success()
        {
            await DeleteCart();
            return View();
        }

        public IActionResult Failure()
        {
            return View();
        }

        private async Task<string> GetPaymentUrl(HttpClient client)
        {
            string cartId = GetCartId();
            var cartResponse = await client.GetAsync($"cart/{cartId}");
            cartResponse.EnsureSuccessStatusCode();
            var cartContent = await cartResponse.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<CartViewModel>(cartContent);
            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Items = cart.Products.Select(p => new PaymentItemRequest
                {
                    Name = p.ProductName,
                    Price = p.Total / p.Quantity,
                    Quantity = p.Quantity
                })
            };
            string jsonBody = JsonConvert.SerializeObject(paymentRequest);
            StringContent body = new StringContent(jsonBody, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("payment", body);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<PaymentUrl>(await response.Content.ReadAsStringAsync()).Url;
        }

        private async Task CreateOrder(HttpClient client, OrderDataViewModel order)
        {
            Guid cartId = Guid.Parse(GetCartId());
            OrderRequest request = new OrderRequest(cartId, order);
            string jsonBody = JsonConvert.SerializeObject(request);
            StringContent body = new StringContent(jsonBody, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("order", body);
            response.EnsureSuccessStatusCode();
        }

        private async Task DeleteCart()
        {
            string cartId = GetCartId();
            var httpClient = factory.CreateClient("SafeShopClient");
            var response = await httpClient.DeleteAsync($"cart/{cartId}");
            response.EnsureSuccessStatusCode();
            Response.Cookies.Delete("SID");
        }

        private string GetCartId()
        {
            if (Request.Cookies.ContainsKey("SID"))
            {
                return Request.Cookies["SID"];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
