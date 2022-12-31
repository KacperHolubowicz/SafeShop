using SafeShop.Repository.Filters;
using SafeShop.Service.DTO.Product;

namespace SafeShop.Service.Infrastructure
{
    public interface IProductService
    {
        Task<ProductGetDTO> GetProductAsync(Guid productId, Guid? cartId);
        Task<PagingWrapper<IEnumerable<ProductGetListDTO>>> GetProductsAsync(ProductPagingFilter pagingFilter);
        Task PostProductAsync(ProductPostDTO product);
        Task PutProductAsync(ProductPutDTO product, Guid id);
        Task DeleteProductAsync(Guid id);
    }
}
