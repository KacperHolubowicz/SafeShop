using SafeShop.Core.Model;

namespace SafeShop.Repository.Infrastructure
{
    public interface IOrderRepository
    {
        public Task AddOrderAsync(Order order);
        public Task<IEnumerable<Order>> FindOrdersAsync(Guid userId);
        public Task<Order> FindOrderAsync(Guid orderId, Guid userId);
    }
}
