using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class OrderDataViewModel
    {
        [Display(Name = "Imię")] public string FirstName { get; set; }
        [Display(Name = "Nazwisko")] public string LastName { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Display(Name = "E-mail")] public string Email { get; set; }
        [Phone]
        [Display(Name = "Numer telefonu")] public string PhoneNumber { get; set; }
        [Display(Name = "Miejscowość")] public string Town { get; set; }
        [Display(Name = "Ulica")] public string Street { get; set; }

        [StringLength(maximumLength: 6, MinimumLength = 6)]
        [RegularExpression(@"\d{2}-\d{3}$")]
        [Display(Name = "Kod pocztowy")] public string ZipCode { get; set; }
    }
}
