using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.Product
{
    public class ProductPostDTO
    {

        [MinLength(1)]
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(120)]
        public string Description { get; set; }

        [RegularExpression("^[a-zA-Z -]+$", ErrorMessage = "Prosimy podawać tylko litery")]
        public string Category { get; set; }

        [Range(1, 10)]
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
    }
}
