using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.Order.Details
{
    public class ShippingDetailsPostDTO
    {
        [MaxLength(40, ErrorMessage = "Nie należy podawać więcej niż 40 znaków swojego imienia")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Nie należy podawać więcej niż 50 znaków swojego nazwiska")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Nie należy podawać więcej niż 50 znaków swojej miejscowości")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string Town { get; set; }

        [MaxLength(30, ErrorMessage = "Nie należy podawać więcej niż 30 znaków swojej ulicy")]
        public string Street { get; set; }

        [StringLength(maximumLength: 6, MinimumLength = 6)]
        [RegularExpression(@"\d{2}-\d{3}$")]
        public string ZipCode { get; set; }
    }
}
