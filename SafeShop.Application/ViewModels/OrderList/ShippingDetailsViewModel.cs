using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels.OrderList
{
    public class ShippingDetailsViewModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Imię")]
        [MaxLength(40, ErrorMessage = "Nie powinieneś podawać więcej niż 40 znaków swojego imienia")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwisko")]
        [MaxLength(50, ErrorMessage = "Nie powinieneś podawać więcej niż 50 znaków swojego nazwiska")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Miasto")]
        [MaxLength(40, ErrorMessage = "Nie powinieneś podawać więcej niż 40 znaków swojej miejscowości")]
        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string Town { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Adres")]
        [MaxLength(40, ErrorMessage = "Nie powinieneś podawać więcej niż 40 znaków swojej ulicy")]
        public string Street { get; set; }

        [StringLength(maximumLength: 6, MinimumLength = 6)]
        [RegularExpression(@"\d{2}-\d{3}$")]
        public string ZipCode { get; set; }
    }
}