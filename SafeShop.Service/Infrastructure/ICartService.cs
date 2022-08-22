using SafeShop.Service.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Infrastructure
{
    public interface ICartService
    {
        Task<CartGetDTO> GetCartAsync(Guid id);
        Task<Guid> PostCartAsync(CartPostDTO? cart);
        Task PutCartAsync(CartPutDTO cart, Guid id);
        Task DeleteCartAsync(Guid id);
    }
}
