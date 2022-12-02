using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class LoginFormViewModel
    {
        [Required (ErrorMessage = "To pole jest wymagane")] public string Login { get; set; }

        [Display(Name = "Hasło")][Required(ErrorMessage = "To pole jest wymagane")]
        public string Password { get; set; }
    }
}
