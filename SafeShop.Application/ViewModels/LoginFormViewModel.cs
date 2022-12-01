using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class LoginFormViewModel
    {
        public string Login { get; set; }
        [Display(Name = "Hasło")]public string Password { get; set; }
    }
}
