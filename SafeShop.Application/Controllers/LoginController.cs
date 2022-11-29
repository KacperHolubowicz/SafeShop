using Microsoft.AspNetCore.Mvc;

namespace SafeShop.Application.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
