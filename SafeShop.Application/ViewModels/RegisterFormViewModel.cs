using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class RegisterFormViewModel
    {
        public string Login { get; set; }
        [Display(Name = "Hasło")] public string Password { get; set; }
        [Display(Name = "Powtórz hasło")] public string RepeatPassword { get; set; }
        [Display(Name = "Adres email")] public string Email { get; set; }
        [Display(Name = "Imię")] public string FirstName { get; set; }
        [Display(Name = "Nazwisko")] public string LastName { get; set; }
    }
}
