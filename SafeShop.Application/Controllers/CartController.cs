﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShop.Application.Requests;
using SafeShop.Application.Responses;
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

        public async Task<IActionResult> EditQuantity(string quantity, Guid productId)
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            ChangeProductQuantityRequest request = new ChangeProductQuantityRequest() { Quantity = int.Parse(quantity) };
            string jsonBody = JsonConvert.SerializeObject(request);
            StringContent body = new StringContent(jsonBody, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"cartproducts/{productId}", body);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Order(Guid id)
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            var cartResponse = await httpClient.GetAsync($"cart/{id}");
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
            var response = await httpClient.PostAsync("payment", body);
            response.EnsureSuccessStatusCode();
            string url = JsonConvert.DeserializeObject<PaymentUrl>(await response.Content.ReadAsStringAsync()).Url;
            return Redirect(url);
        }
    }
}
