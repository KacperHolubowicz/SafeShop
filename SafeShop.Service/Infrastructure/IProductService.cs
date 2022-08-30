using SafeShop.Repository.Filters;
using SafeShop.Service.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Infrastructure
{
    public interface IProductService
    {
        Task<ProductGetDTO> GetProductAsync(Guid id);
        Task<PagingWrapper<IEnumerable<ProductGetListDTO>>> GetProductsAsync(ProductPagingFilter pagingFilter);
        Task PostProductAsync(ProductPostDTO product);
        Task PutProductAsync(ProductPutDTO product, Guid id);
        Task DeleteProductAsync(Guid id);
    }
}
