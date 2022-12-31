using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.User
{
    public class UserPutDTO
    {
        [MaxLength(60, ErrorMessage = "Prosimy stosować email krótszy niż 60 znaków")]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(40, ErrorMessage = "Nie należy podawać więcej niż 40 znaków swojego imienia")]
        public string? FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Nie należy podawać więcej niż 50 znaków swojego nazwiska")]
        public string? LastName { get; set; }
    }
}
