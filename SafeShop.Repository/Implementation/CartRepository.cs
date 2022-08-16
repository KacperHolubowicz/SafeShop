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
    public class CartRepository : ICartRepository
    {
        private readonly SafeShopContext context;

        public CartRepository(SafeShopContext context)
        {
            this.context = context;
        }

        public async Task<Cart> FindCartAsync(Guid id)
        {
            Cart cart = await context.Carts.FindAsync(id);
            return cart;
        }

        public async Task AddCartAsync(Cart cart)
        {
            await context.Carts.AddAsync(cart);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            Cart oldCart = await context.Carts.FindAsync(cart.ID);
            if(oldCart == null)
            {
                throw new NullReferenceException("No such resource to update");
            }
            oldCart.Products = cart.Products;
            oldCart.ModifiedAt = DateTime.Now;
            oldCart.Total = CalculateTotal(cart.Products);
            context.Carts.Update(oldCart);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(Guid id)
        {
            Cart cart = await context.Carts.FindAsync(id);
            if(cart != null)
            {
                context.Carts.Remove(cart);
            }
        }

        private decimal CalculateTotal(IEnumerable<CartProduct> products)
        {
            return products.Sum(product => product.Total);
        }
    }
}
