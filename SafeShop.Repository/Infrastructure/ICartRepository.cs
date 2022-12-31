using SafeShop.Core.Model;

namespace SafeShop.Repository.Infrastructure
{
    public interface ICartRepository
    {
        Task<Cart> FindCartAsync(Guid id);
        Task<Guid> AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart, Guid id);
        Task DeleteCartAsync(Guid id);
        Task DeleteEmptyCartAsync(Guid productId); 
    }
}
