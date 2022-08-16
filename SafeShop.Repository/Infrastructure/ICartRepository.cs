using SafeShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Repository.Infrastructure
{
    public interface ICartRepository
    {
        Task<Cart> FindCartAsync(Guid id);
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(Guid id);
    }
}
