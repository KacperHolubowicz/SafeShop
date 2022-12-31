using SafeShop.Core.Model;

namespace SafeShop.Repository.Infrastructure
{
    public interface ICartProductRepository
    {
        Task AddCartProductAsync(CartProduct cartProduct);
        Task UpdateCartProductAsync(CartProduct cartProduct, Guid id);
        Task DeleteCartProductAsync(Guid id);
    }
}
