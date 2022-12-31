using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.Auth
{
    public class LoginCredentials
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Login wymaga co najmniej 6 znaków")]
        [MaxLength(20, ErrorMessage = "Login nie może być dłuższy od 20 znaków")]
        public string Login { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło wymaga co najmniej 6 znaków")]
        [MaxLength(30, ErrorMessage = "Hasło nie może być dłuższe od 30 znaków")]
        public string Password { get; set; }
    }
}
