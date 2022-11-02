using SafeShop.Service.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Infrastructure
{
    public interface IOrderService
    {
        public Task PostOrderAsync(OrderPostDTO order);
        public Task<IEnumerable<OrderListDTO>> GetOrdersAsync(Guid userId);
        public Task<OrderGetDTO> GetOrderAsync(Guid orderId, Guid userId);
    }
}
