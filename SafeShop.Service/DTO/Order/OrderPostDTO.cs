using SafeShop.Service.DTO.Order.Details;

namespace SafeShop.Service.DTO.Order
{
    public class OrderPostDTO
    {
        public Guid CartID { get; set; }
        public OrderDetailsPostDTO Details { get; set; }
    }
}
