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
        Task<Guid> PostCartProductAsync(CartProductPostDTO cartProduct, Guid? userId);
        Task PutCartProductAsync(CartProductPutDTO cartProduct, Guid id);
        Task DeleteCartProductAsync(Guid id);
    }
}
