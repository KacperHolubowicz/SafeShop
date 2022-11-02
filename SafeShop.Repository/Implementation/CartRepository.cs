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
    public class CartRepository : ICartRepository
    {
        private readonly SafeShopContext context;

        public CartRepository(SafeShopContext context)
        {
            this.context = context;
        }

        public async Task<Cart> FindCartAsync(Guid id)
        {
            Cart cart = await context.Carts
                .Include(c => c.User)
                .Include(c => c.Products)
                .ThenInclude(cp => cp.Product)
                .FirstAsync(c => c.ID == id);
            return cart;
        }

        public async Task<Guid> AddCartAsync(Cart cart)
        {
            await context.Carts.AddAsync(cart);
            await context.SaveChangesAsync();
            return cart.ID;
        }

        public async Task UpdateCartAsync(Cart cart, Guid id)
        {
            Cart oldCart = await context.Carts.FindAsync(id);
            if(oldCart == null)
            {
                throw new NullReferenceException("No such resource to update");
            }
            oldCart.Products = cart.Products;
            oldCart.ModifiedAt = DateTime.Now;
            context.Carts.Update(oldCart);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(Guid id)
        {
            Cart cart = await context.Carts.FindAsync(id);
            if(cart != null)
            {
                context.Carts.Remove(cart);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmptyCartAsync(Guid productId)
        {
            CartProduct cartP = await context.CartProducts.FindAsync(productId);
            Cart cart = cartP.Cart;
            context.Carts.Remove(cart);
            await context.SaveChangesAsync();
        }

        private decimal CalculateTotal(IEnumerable<CartProduct> products)
        {
            return products.Sum(product => product.Total);
        }
    }
}
