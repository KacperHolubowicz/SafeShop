using SafeShop.Service.DTO.CartProduct;

namespace SafeShop.Service.Infrastructure
{
    public interface ICartProductService
    {
        Task<Guid> PostCartProductAsync(CartProductPostDTO cartProduct, Guid? userId);
        Task PutCartProductAsync(CartProductPutDTO cartProduct, Guid id);
        Task DeleteCartProductAsync(Guid id);
    }
}
