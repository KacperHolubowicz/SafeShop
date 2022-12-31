using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.Order.Details
{
    public class BillingDetailsPostDTO
    {
        [MaxLength(40, ErrorMessage = "Nie należy podawać więcej niż 40 znaków swojego imienia")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Nie należy podawać więcej niż 50 znaków swojego nazwiska")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string LastName { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
