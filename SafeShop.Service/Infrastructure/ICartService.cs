using SafeShop.Service.DTO.Cart;

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
