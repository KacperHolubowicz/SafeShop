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
        Task AddCartProductAsync(CartProduct cartProduct);
        Task UpdateCartProductAsync(CartProduct cartProduct, Guid id);
        Task DeleteCartProductAsync(Guid id);
    }
}
