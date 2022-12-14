using Microsoft.EntityFrameworkCore;
using SafeShop.Core.Model;
using SafeShop.Repository.DataAccess;
using SafeShop.Repository.Infrastructure;

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
            order.Details.Status = "Pending";
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

        public async Task<Order> FindOrderAsync(Guid orderId, Guid userId)
        {
            Order order = await context.Orders
                .Include(o => o.User)
                .Include(o => o.Products)
                .ThenInclude(p => p.Product)
                .Include(o => o.Details)
                .ThenInclude(d => d.Shipping)
                .Include(o => o.Details)
                .ThenInclude(d => d.Billing)
                .FirstAsync(o => o.ID == orderId);
            if(order.User.ID != userId)
            {
                return null;
            }
            return order;
        }
    }
}
