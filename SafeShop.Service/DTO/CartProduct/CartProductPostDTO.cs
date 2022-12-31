using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.CartProduct
{
    public class CartProductPostDTO
    {
        [Required]
        [Range(1, 10, ErrorMessage = "Prosimy dodawać między 1 a 10 produktów")]
        public int Quantity { get; set; }
        public Guid ProductID { get; set; }
        public Guid? CartID { get; set; }
        public Guid? UserID { get; set; } = Guid.Empty;
    }
}
