using SafeShop.Application.ViewModels.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class RegisterFormViewModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Login wymaga co najmniej 6 znaków")]
        [MaxLength(20, ErrorMessage = "Login nie może być dłuższy od 20 znaków")]
        public string Login { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Hasło")]
        [StrongPassword(6, 30)]
        public string Password { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Powtórz hasło")]
        [PasswordConfirmation]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Adres email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Imię")]
        [MaxLength(40, ErrorMessage = "Nie powinieneś podawać więcej niż 40 znaków swojego imienia")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwisko")]
        [MaxLength(50, ErrorMessage = "Nie powinieneś podawać więcej niż 40 znaków swojego nazwiska")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string LastName { get; set; }
    }
}
