using System.ComponentModel.DataAnnotations;

namespace SafeShop.Core.Model
{
    public class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Login wymaga co najmniej 6 znaków")]
        [MaxLength(20, ErrorMessage = "Login nie może być dłuższy od 20 znaków")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło wymaga co najmniej 6 znaków")]
        [MaxLength(30, ErrorMessage = "Hasło nie może być dłuższe od 30 znaków")]
        public byte[] Password { get; set; } = default!;

        [Required(ErrorMessage = "To pole jest wymagane")]
        public byte[] Salt { get; set; } = default!;

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(60, ErrorMessage = "Prosimy stosować email krótszy niż 60 znaków")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(40, ErrorMessage = "Nie należy podawać więcej niż 40 znaków swojego imienia")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MaxLength(50, ErrorMessage = "Nie należy podawać więcej niż 50 znaków swojego nazwiska")]
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }
    }
}
