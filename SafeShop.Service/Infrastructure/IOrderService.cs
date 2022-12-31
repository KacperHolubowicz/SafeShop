using SafeShop.Service.DTO.Order;

namespace SafeShop.Service.Infrastructure
{
    public interface IOrderService
    {
        public Task PostOrderAsync(OrderPostDTO order);
        public Task<IEnumerable<OrderListDTO>> GetOrdersAsync(Guid userId);
        public Task<OrderGetDTO> GetOrderAsync(Guid orderId, Guid userId);
    }
}
