using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafeShop.Application.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SafeShop.Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory factory;

        public LoginController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginFormViewModel loginForm)
        {
            var client = factory.CreateClient("SafeShopClient");
            string jsonBody = JsonConvert.SerializeObject(loginForm);
            StringContent body = new StringContent(jsonBody, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("auth/login", body);
            response.EnsureSuccessStatusCode();
            string token = await response.Content.ReadAsStringAsync();
            TokenToCookie(token);
            await SetClaims(token);
            return Redirect("/");
        }

        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        private void TokenToCookie(string token)
        {     
            CookieOptions options = new CookieOptions();
            options.HttpOnly = true;
            options.Secure = true;
            options.SameSite = SameSiteMode.Strict;
            options.Expires = DateTimeOffset.UtcNow.AddHours(1);
            Response.Cookies.Append("AccessID", token, options);
        }

        private async Task SetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var claims = new List<Claim>();
            claims.Add(tokenS.Claims.First(claim => claim.Type == ClaimTypes.Email));
            claims.Add(tokenS.Claims.First(claim => claim.Type == ClaimTypes.Surname));
            claims.Add(tokenS.Claims.First(claim => claim.Type == ClaimTypes.Role));
            claims.Add(tokenS.Claims.First(claim => claim.Type == "ID"));
            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
        }
    }
}
