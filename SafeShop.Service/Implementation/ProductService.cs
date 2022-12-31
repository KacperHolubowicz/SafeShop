using AutoMapper;
using SafeShop.Core.Model;
using SafeShop.Repository.Filters;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.Product;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;

namespace SafeShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IResizerService resizer;

        public ProductService(IProductRepository productRepository, IMapper mapper, IResizerService resizer)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.resizer = resizer;
        }

        public async Task<ProductGetDTO> GetProductAsync(Guid productId, Guid? cartId)
        {
            Product product = await productRepository.FindProductAsync(productId);
            if(product == null)
            {
                throw new ResourceNotFoundException();
            }
            var prod = mapper.Map<ProductGetDTO>(product);
            if (cartId.HasValue) {
                prod.IsInCart = await productRepository.IsProductInCart(productId, cartId.Value);
            }
            return prod;
        }

        public async Task<PagingWrapper<IEnumerable<ProductGetListDTO>>> GetProductsAsync(ProductPagingFilter pagingFilter)
        {
            try
            {
                PagingWrapper<IEnumerable<Product>> productWrapper = await productRepository.FindProductsAsync(pagingFilter);
                var products = productWrapper.PaginatedProperty;
                if (products.Count() == 0)
                {
                    return new PagingWrapper<IEnumerable<ProductGetListDTO>>(Enumerable.Empty<ProductGetListDTO>(), false, false);
                }
                var prods = mapper.Map<IEnumerable<ProductGetListDTO>>(products);
                return new PagingWrapper<IEnumerable<ProductGetListDTO>>(prods, 
                    productWrapper.HasPreviousPage, productWrapper.HasNextPage);
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
                productEntity.Image = resizer.ResizeImage(productEntity.Image);
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
                productEntity.Image = resizer.ResizeImage(productEntity.Image);
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
