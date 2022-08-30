using SafeShop.Core.Model;
using SafeShop.Repository.Filters;

namespace SafeShop.Repository.Infrastructure
{
    public interface IProductRepository
    {
        Task<Product> FindProductAsync(Guid id);
        Task<PagingWrapper<IEnumerable<Product>>> FindProductsAsync(ProductPagingFilter pagingFilter);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product, Guid id);
        Task RemoveProductAsync(Guid id);
    }
}
