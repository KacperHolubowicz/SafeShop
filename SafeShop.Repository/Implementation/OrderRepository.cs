using Microsoft.EntityFrameworkCore;
using SafeShop.Core.Model;
using SafeShop.Repository.DataAccess;
using SafeShop.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SafeShopContext context;

        public OrderRepository(SafeShopContext context)
        {
            this.context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            order.CreatedAt = DateTime.UtcNow;
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> FindOrdersAsync(Guid userId)
        {
            var orders = await context.Orders
                .Include(o => o.Details)
                .Include(o => o.Products)
                .Where(o => o.User != null && o.User.ID == userId)
                .ToListAsync();
            return orders;
        }

        public async Task<Order> FindOrderAsync(Guid orderId)
        {
            Order order = await context.Orders
                .Include(o => o.Products)
                .ThenInclude(p => p.Product)
                .Include(o => o.Details)
                .ThenInclude(d => d.Shipping)
                .Include(o => o.Details)
                .ThenInclude(d => d.Billing)
                .FirstAsync(o => o.ID == orderId);
            return order;
        }
    }
}
