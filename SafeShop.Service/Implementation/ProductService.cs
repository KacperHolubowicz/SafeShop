using SafeShop.Repository.Filters;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.Product;
using SafeShop.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Implementation
{
    //TODO map entity->dto with automapper, maybe its time to finally learn it
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Task<ProductGetDTO> GetProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductGetListDTO>> GetProductsAsync(ProductPagingFilter pagingFilter)
        {
            throw new NotImplementedException();
        }

        public Task PostProductAsync(ProductPostDTO product)
        {
            throw new NotImplementedException();
        }

        public Task PutProductAsync(ProductPutDTO product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
