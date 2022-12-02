using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SafeShop.Application.Requests.Auth;
using SafeShop.Application.ViewModels;

namespace SafeShop.Application.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory factory;

        public RegisterController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(RegisterFormViewModel registerForm)
        {
            if(!ModelState.IsValid)
            {
                return View(registerForm);
            }
            RegisterRequest request = new RegisterRequest(registerForm);
            var httpClient = factory.CreateClient("SafeShopClient");
            string jsonBody = JsonConvert.SerializeObject(request);
            StringContent body = new StringContent(jsonBody, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"auth/register", body);
            response.EnsureSuccessStatusCode();
            TempData["RegisterMessage"] = "Pomyślnie zarejestrowano konto. Możesz teraz się zalogować.";
            return RedirectToAction("Index", "Login");
        }
    }
}
