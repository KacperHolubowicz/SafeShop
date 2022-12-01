using Microsoft.AspNetCore.Mvc;

namespace SafeShop.Application.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
