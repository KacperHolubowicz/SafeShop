using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class CartProductViewModel
    {
        public Guid ID { get; set; }
        [Display(Name = "Ilość")] public int Quantity { get; set; }
        [Display(Name = "Cena łączna")] public decimal Total { get; set; }
        [Display(Name = "Nazwa produktu")] public string ProductName { get; set; }
    }
}
