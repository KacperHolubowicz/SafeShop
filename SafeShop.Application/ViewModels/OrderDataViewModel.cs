using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class OrderDataViewModel
    {
        [Display(Name = "Imię")][Required(ErrorMessage = "To pole jest wymagane")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")][Required(ErrorMessage = "To pole jest wymagane")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail")][Required(ErrorMessage = "To pole jest wymagane")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Numer telefonu")][Required(ErrorMessage = "To pole jest wymagane")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Miejscowość")][Required(ErrorMessage = "To pole jest wymagane")]
        public string Town { get; set; }

        [Display(Name = "Ulica")][Required(ErrorMessage = "To pole jest wymagane")]
        public string Street { get; set; }


        [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Nieprawidłowy kod pocztowy")]
        [RegularExpression(@"\d{2}-\d{3}$", ErrorMessage = "Nieprawidłowy kod pocztowy")]
        [Display(Name = "Kod pocztowy")][Required(ErrorMessage = "To pole jest wymagane")]
        public string ZipCode { get; set; }
    }
}
