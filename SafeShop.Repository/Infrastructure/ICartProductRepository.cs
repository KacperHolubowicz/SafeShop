using SafeShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Repository.Infrastructure
{
     public interface ICartProductRepository
    {
        Task<IEnumerable<CartProduct>> FindCartProductsAsync();
        Task AddCartProductAsync(CartProduct cartProduct);
        Task UpdateCartProductAsync(CartProduct cartProduct);
        Task DeleteCartProductAsync(Guid id);
    }
}
