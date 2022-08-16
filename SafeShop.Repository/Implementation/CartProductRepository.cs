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
    public class CartProductRepository : ICartProductRepository
    {
        private readonly SafeShopContext context;

        public CartProductRepository(SafeShopContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CartProduct>> FindCartProductsAsync()
        {
            List<CartProduct> products = await context.CartProducts.ToListAsync();
            return products;
        }

        public async Task AddCartProductAsync(CartProduct cartProduct)
        {
            await context.CartProducts.AddAsync(cartProduct);
            await context.SaveChangesAsync();

        }

        public async Task UpdateCartProductAsync(CartProduct cartProduct)
        {
            CartProduct oldProduct = await context.CartProducts.FindAsync(cartProduct.ID);
            if(oldProduct == null)
            {
                throw new NullReferenceException("No such resource to update");
            }
            oldProduct.Quantity = cartProduct.Quantity;
            oldProduct.Total = CalculateTotal(cartProduct);
            context.Update(oldProduct);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCartProductAsync(Guid id)
        {
            var cartProduct = await context.CartProducts.FindAsync(id);
            if(cartProduct != null)
            {
                context.Remove(cartProduct);
            }
        }

        private decimal CalculateTotal(CartProduct product)
        {
            return product.Quantity * product.Product.Price;
        }
    }
}
