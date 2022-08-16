using Microsoft.EntityFrameworkCore;
using SafeShop.Core.Model;
using SafeShop.Repository.DataAccess;
using SafeShop.Repository.Filters;
using SafeShop.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly SafeShopContext context;

        public ProductRepository(SafeShopContext context)
        {
            this.context = context;
        }

        public async Task<Product> FindProductAsync(Guid id)
        {
            Product product = await context.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> FindProductsAsync(ProductPagingFilter pagingFilter)
        {
            int maxPage = (int)Math.Round((double)(context.Products.Count() / pagingFilter.PageSize), 0);
            int page = pagingFilter.CurrentPage > maxPage ? maxPage : pagingFilter.CurrentPage;
            int size = pagingFilter.PageSize;

            return await context.Products
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        { 
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            Guid id = product.ID;
            Product oldProduct = await context.Products.FindAsync(id);
            if(oldProduct == null)
            {
                throw new NullReferenceException("No such resource to update");
            }
            oldProduct.Name = product.Name ?? oldProduct.Name;
            oldProduct.Description = product.Description ?? oldProduct.Description;
            oldProduct.Category = product.Category ?? oldProduct.Category;
            oldProduct.Price = product.Price == decimal.Zero ? oldProduct.Price : product.Price;
            context.Products.Update(oldProduct);
            await context.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(Guid id)
        {
            Product product = await context.Products.FindAsync(id);
            if (product != null)
            {
                context.Products.Remove(product);
            }
        }
    }
}
