using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShop.Application.ViewModels;

namespace SafeShop.Application.Controllers
{
    public class ShopController : Controller
    {
        private readonly IHttpClientFactory factory;

        public ShopController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            var response = await httpClient.GetAsync($"products?page={pageNumber}&size=15");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<ProductListPageModel>(content);
            ViewData["PageNumber"] = pageNumber;
            return View(products);
        }
    }
}
