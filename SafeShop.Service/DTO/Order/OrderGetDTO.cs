using SafeShop.Service.DTO.Order.Details;

namespace SafeShop.Service.DTO.Order
{
    public class OrderGetDTO
    {
        public Guid ID { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<OrderProductGetDTO> Products { get; set; }
        public OrderDetailsGetDTO Details { get; set; }
    }
}
