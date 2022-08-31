using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShop.Application.ViewModels;

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
            CartViewModel cart = new CartViewModel() { Products = Enumerable.Empty<CartProductViewModel>() };
            if(Request.Cookies.ContainsKey("SID"))
            {
                string cartId = Request.Cookies["SID"].ToString();
                var response = await httpClient.GetAsync($"cart/{cartId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                cart = JsonConvert.DeserializeObject<CartViewModel>(content);
            }
            return View(cart);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            var response = await httpClient.DeleteAsync($"cartproducts/{id}");
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
