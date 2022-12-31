using SafeShop.Service.DTO.CartProduct;

namespace SafeShop.Service.DTO.Cart
{
    public class CartGetDTO
    {
        public Guid ID { get; set; }
        public IEnumerable<CartProductGetDTO> Products { get; set; }
    }
}
