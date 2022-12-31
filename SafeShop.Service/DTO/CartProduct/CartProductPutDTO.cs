using System.ComponentModel.DataAnnotations;

namespace SafeShop.Service.DTO.CartProduct
{
    public class CartProductPutDTO
    {
        [Range(1, 10, ErrorMessage = "Prosimy dodawać między 1 a 10 produktów")]
        public int Quantity { get; set; }
    }
}
