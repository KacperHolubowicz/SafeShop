using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels
{
    public class ProductDetailsPageModel
    {
        public Guid ID { get; set; }
        [Display(Name = "Nazwa")] public string Name { get; set; }
        [Display(Name = "Opis")] public string Description { get; set; }
        [Display(Name = "Kategoria")] public string Category { get; set; }
        [Display(Name = "Cena")] public decimal Price { get; set; }
        [Display(Name = "Obraz")] public byte[] Image { get; set; }
        public bool IsInCart { get; set; }
    }
}
