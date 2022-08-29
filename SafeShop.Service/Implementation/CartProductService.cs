using AutoMapper;
using SafeShop.Core.Model;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.CartProduct;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Implementation
{
    public class CartProductService : ICartProductService
    {
        private readonly ICartProductRepository cartProductRepository;
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public CartProductService(ICartProductRepository cartProductRepository, ICartRepository cartRepository,
            IMapper mapper, IProductRepository productRepository)
        {
            this.cartProductRepository = cartProductRepository;
            this.cartRepository = cartRepository;
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<CartProductGetDTO>> FindCartProductsAsync(Guid cartId)
        {
            return mapper.Map<IEnumerable<CartProductGetDTO>>(await cartProductRepository.FindCartProductsAsync(cartId));
        }

        public async Task<Guid> AddCartProductAsync(CartProductPostDTO cartProduct, Guid? userId)
        {
            try
            {
                Guid cartId;
                CartProduct product = new CartProduct()
                {
                    AddedAt = DateTime.UtcNow,
                    Product = await productRepository.FindProductAsync(cartProduct.ProductID),
                    Quantity = cartProduct.Quantity,
                    Total = cartProduct.Total
                };

                if(cartProduct.CartID == null)
                {
                    Cart newCart = new Cart()
                    {
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        Products = new List<CartProduct>(),
                        Total = cartProduct.Total,
                        UserID = userId
                    };
                    product.Cart = newCart;
                    newCart.Products.Append(product);
                    cartId = await cartRepository.AddCartAsync(newCart);
                } 
                else
                {
                    cartId = cartProduct.CartID.Value;
                    product.Cart = await cartRepository.FindCartAsync(cartId);
                    await cartProductRepository.AddCartProductAsync(product);
                }
                return cartId;
            } catch(Exception ex)
            {
                throw new AddingResourceException(ex.Message);
            }
        }

        public async Task UpdateCartProductAsync(CartProductPutDTO cartProduct, Guid id)
        {
            try
            {
                CartProduct cartProductEntity = mapper.Map<CartProduct>(cartProduct);
                await cartProductRepository.UpdateCartProductAsync(cartProductEntity, id);
            }
            catch (NullReferenceException ex)
            {
                throw new ResourceNotFoundException(ex.Message);
            }
            catch (Exception ex2)
            {
                throw new UpdatingResourceException(ex2.Message);
            }
        }

        public async Task DeleteCartProductAsync(Guid id)
        {
            try
            {
                await cartProductRepository.DeleteCartProductAsync(id);
            }
            catch (Exception ex)
            {
                throw new ResourceNotFoundException(ex.Message);
            }
        }
    }
}
