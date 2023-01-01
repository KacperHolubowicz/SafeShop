using SafeShop.Application.ViewModels.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.User
{
    public class UserPostDTO
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Login wymaga co najmniej 6 znaków")]
        [MaxLength(20, ErrorMessage = "Login nie może być dłuższy od 20 znaków")]
        public string Login { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [StrongPassword(10, 30)]
        public string Password { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [PasswordConfirmation]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(60, ErrorMessage = "Prosimy stosować email krótszy niż 60 znaków")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(40, ErrorMessage = "Nie należy podawać więcej niż 40 znaków swojego imienia")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(50, ErrorMessage = "Nie należy podawać więcej niż 50 znaków swojego nazwiska")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string LastName { get; set; }
    }
}
