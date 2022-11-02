using AutoMapper;
using SafeShop.Core.Model;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.Order;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        public async Task PostOrderAsync(OrderPostDTO order)
        {
            try
            {
                Cart cart = await cartRepository.FindCartAsync(order.CartID);
                Order orderEntity = new Order()
                {
                    CreatedAt = DateTime.UtcNow,
                    Details = mapper.Map<OrderDetails>(order.Details),
                    Total = cart.Products.Sum(p => p.Total),
                    User = cart.User,
                    Products = mapper.Map<IEnumerable<OrderProduct>>(cart.Products)
                };
                await orderRepository.AddOrderAsync(orderEntity);
            }
            catch(Exception ex)
            {
                throw new AddingResourceException(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderListDTO>> FindOrdersAsync(Guid userId)
        {
            var order = await orderRepository.FindOrdersAsync(userId);
            return mapper.Map<IEnumerable<OrderListDTO>>(order);
        }

        public async Task<OrderGetDTO> FindOrderAsync(Guid orderId)
        {
            var order = await orderRepository.FindOrderAsync(orderId);
            if(order == null)
            {
                throw new ResourceNotFoundException();
            }
            return mapper.Map<OrderGetDTO>(order);
        }
    }
}
