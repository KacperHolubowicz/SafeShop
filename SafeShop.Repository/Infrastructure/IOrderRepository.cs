using SafeShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Repository.Infrastructure
{
    public interface IOrderRepository
    {
        public Task AddOrderAsync(Order order);
        public Task<IEnumerable<Order>> FindOrdersAsync(Guid userId);
        public Task<Order> FindOrderAsync(Guid orderId, Guid userId);
    }
}
