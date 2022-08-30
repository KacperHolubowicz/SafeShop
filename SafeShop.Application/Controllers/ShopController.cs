﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShop.Application.Requests;
using SafeShop.Application.Responses;
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

        public async Task<IActionResult> Details(Guid id)
        {
            var httpClient = factory.CreateClient("SafeShopClient");
            var response = await httpClient.GetAsync($"products/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDetailsPageModel>(content);
            return View(product);
        }

        public async Task<IActionResult> AddToCart(AddProductToCartRequest request)
        {
            if (Request.Cookies.ContainsKey("SID"))
            {
                request.CartID = Guid.Parse(HttpContext.Request.Cookies["SID"].ToString());
            }
            var httpClient = factory.CreateClient("SafeShopClient");
            string jsonBody = JsonConvert.SerializeObject(request);
            StringContent body = new StringContent(jsonBody, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"cartproducts", body);
            response.EnsureSuccessStatusCode();
            if (!Request.Cookies.ContainsKey("SID"))
            {
                var content = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<AddProductToCartResponse>(content);
                CookieOptions options = new CookieOptions();
                options.HttpOnly = true;
                options.Secure = true;
                options.Expires = DateTimeOffset.UtcNow.AddDays(7);
                Response.Cookies.Append("SID", resp.CartID.ToString(), options);
            }
            return RedirectToAction("Index");
        }
    }
}