using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShop.Application.Requests;
using SafeShop.Application.ViewModels;
using System.Net.Http.Headers;

namespace SafeShop.Application.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpClientFactory factory;

        public CartController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            SetBearerToken(httpClient);
            CartViewModel cart = new CartViewModel() { Products = Enumerable.Empty<CartProductViewModel>() };
            string cartId = GetCartId();
            if (cartId != string.Empty)
            {
                var response = await httpClient.GetAsync($"cart/{cartId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                cart = JsonConvert.DeserializeObject<CartViewModel>(content);
            }
            return View(cart);
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            SetBearerToken(httpClient);
            var response = await httpClient.DeleteAsync($"cartproducts/{id}");
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditQuantity(string quantity, Guid productId)
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            SetBearerToken(httpClient);
            ChangeProductQuantityRequest request = new ChangeProductQuantityRequest() { Quantity = int.Parse(quantity) };
            string jsonBody = JsonConvert.SerializeObject(request);
            StringContent body = new StringContent(jsonBody, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"cartproducts/{productId}", body);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        public IActionResult Order()
        {
            return RedirectToAction("Index", "Order");
        }

        private string GetCartId()
        {
            if(Request.Cookies.ContainsKey("SID"))
            {
                return Request.Cookies["SID"];
            } else
            {
                return string.Empty;
            }
        }

        private void SetBearerToken(HttpClient httpClient)
        {
            if(Request.Cookies.ContainsKey("AccessID"))
            {
                string accessToken = Request.Cookies["AccessID"].ToString();
                httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue($"Bearer", $"{accessToken}");

            }
        }
    }
}
