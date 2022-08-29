using SafeShop.Service.DTO.CartProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Infrastructure
{
    public interface ICartProductService
    {
        Task<IEnumerable<CartProductGetDTO>> FindCartProductsAsync(Guid cartId);
        Task<Guid> AddCartProductAsync(CartProductPostDTO cartProduct, Guid? userId);
        Task UpdateCartProductAsync(CartProductPutDTO cartProduct, Guid id);
        Task DeleteCartProductAsync(Guid id);
    }
}
