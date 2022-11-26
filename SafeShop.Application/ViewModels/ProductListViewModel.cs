using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class ProductListViewModel
    {
        public Guid ID { get; set; }
        [Display(Name = "Nazwa")] public string Name { get; set; }
        [Display(Name = "Kategoria")] public string Category { get; set; }
        [Display(Name = "Cena")] public decimal Price { get; set; }
        [Display(Name = "Obraz")] public byte[] Image { get; set; }
    }
}
