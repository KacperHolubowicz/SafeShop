using AutoMapper;
using SafeShop.Core.Model;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.Cart;
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
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductRepository productRepository;

        public CartService(ICartRepository cartRepository, IUserRepository userRepository,
            IProductRepository productRepository)
        {
            this.cartRepository = cartRepository;
            this.userRepository = userRepository;
            this.productRepository = productRepository;
        }

        public async Task<CartGetDTO> GetCartAsync(Guid id)
        {
            Cart cart = await cartRepository.FindCartAsync(id);
            if(cart == null)
            {
                throw new ResourceNotFoundException();
            }
            return new CartGetDTO()
            {
                ID = cart.ID,
                Products = cart.Products.Select(p => new CartProductGetDTO()
                {
                    Total = p.Total,
                    ID = p.ID,
                    AddedAt = p.AddedAt,
                    ProductName = p.Product.Name,
                    Quantity = p.Quantity
                })
            };
        }

        public async Task<Guid> PostCartAsync(CartPostDTO? cart)
        {
            Cart newCart = new Cart()
            {
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            try
            {
                if (cart != null && cart.UserID != null)
                {
                    cart.UserID = cart.UserID;
                    newCart.User = await userRepository.FindUserAsync(cart.UserID.Value);
                }
                Guid cartId = await cartRepository.AddCartAsync(newCart);
                return cartId;
            } catch (Exception ex)
            {
                throw new AddingResourceException(ex.Message);
            }
        }

        public async Task PutCartAsync(CartPutDTO cart, Guid id)
        {
            try
            {
                Cart productCart = await cartRepository.FindCartAsync(id);
                IEnumerable<CartProduct> cartProducts = cart.Products.Select(p => new CartProduct()
                {
                    Cart = productCart,
                    AddedAt = DateTime.UtcNow,
                    Product = productRepository.FindProductAsync(p.ProductID).Result,
                    Quantity = p.Quantity

                });
                Cart cartEntity = new Cart()
                {
                    Products = cartProducts
                };
                await cartRepository.UpdateCartAsync(cartEntity, id);
            } catch(Exception ex)
            {
                throw new UpdatingResourceException(ex.Message);
            }
        }

        public async Task DeleteCartAsync(Guid id)
        {
            try
            {
                await cartRepository.DeleteCartAsync(id);
            } catch (Exception ex)
            {
                throw new ResourceNotFoundException(ex.Message);
            }
        }
    }
}
