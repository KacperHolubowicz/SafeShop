using SafeShop.Service.DTO.CartProduct;

namespace SafeShop.Service.DTO.Cart
{
    public class CartPutDTO
    {
        public IEnumerable<CartProductPostDTO> Products { get; set; }
    }
}
