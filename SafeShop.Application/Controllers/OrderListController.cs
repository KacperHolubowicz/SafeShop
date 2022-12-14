using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShop.Application.ViewModels;
using SafeShop.Application.ViewModels.OrderList;
using System.Net.Http.Headers;

namespace SafeShop.Application.Controllers
{
    [Authorize]
    public class OrderListController : Controller
    {
        private readonly IHttpClientFactory factory;

        public OrderListController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public async Task<IActionResult> Index()
        {
            var client = factory.CreateClient("SafeShopClient");
            SetBearerToken(client);
            var response = await client.GetAsync($"order/{GetUserID()}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<OrderListViewModel> orderList = 
                JsonConvert.DeserializeObject<IEnumerable<OrderListViewModel>>(content);
            return View(orderList);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var client = factory.CreateClient("SafeShopClient");
            SetBearerToken(client);
            var response = await client.GetAsync($"order/{GetUserID()}/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            OrderViewModel orderDetails = JsonConvert.DeserializeObject<OrderViewModel>(content);
            return View(orderDetails);
        }
        private void SetBearerToken(HttpClient httpClient)
        {
            if (Request.Cookies.ContainsKey("AccessID"))
            {
                string accessToken = Request.Cookies["AccessID"].ToString();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue($"Bearer", $"{accessToken}");

            }
        }

        private Guid GetUserID()
        {
            Guid userId = Guid.Parse(User.Claims.First(claim => claim.Type == "ID").Value);
            return userId;
        }
    }
}
