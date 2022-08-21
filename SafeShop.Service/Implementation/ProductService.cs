using AutoMapper;
using SafeShop.Core.Model;
using SafeShop.Repository.Filters;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.Product;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductGetDTO> GetProductAsync(Guid id)
        {
            Product product = await productRepository.FindProductAsync(id);
            if(product == null)
            {
                throw new ResourceNotFoundException();
            }
            return mapper.Map<ProductGetDTO>(product);
        }

        public async Task<IEnumerable<ProductGetListDTO>> GetProductsAsync(ProductPagingFilter pagingFilter)
        {
            try
            {
                IEnumerable<Product> products = await productRepository.FindProductsAsync(pagingFilter);
                if (products.Count() == 0)
                {
                    return Enumerable.Empty<ProductGetListDTO>();
                }
                return mapper.Map<IEnumerable<ProductGetListDTO>>(products);
            } catch(Exception ex)
            {
                throw new ResourceNotFoundException(ex.Message);
            }
        }

        public async Task PostProductAsync(ProductPostDTO product)
        {
            Product productEntity = mapper.Map<Product>(product);
            try
            {
                await productRepository.AddProductAsync(productEntity);
            } catch(Exception ex)
            {
                throw new AddingResourceException(ex.Message);
            }
        }

        public async Task PutProductAsync(ProductPutDTO product, Guid id)
        {
            Product productEntity = mapper.Map<Product>(product);
            try
            {
                await productRepository.UpdateProductAsync(productEntity, id);
            } catch(Exception ex)
            {
                throw new UpdatingResourceException(ex.Message);
            }
        }

        public async Task DeleteProductAsync(Guid id)
        {
            try
            {
                await productRepository.RemoveProductAsync(id);
            } catch (Exception ex)
            {
                throw new ResourceNotFoundException(ex.Message);
            }
        }
    }
}
