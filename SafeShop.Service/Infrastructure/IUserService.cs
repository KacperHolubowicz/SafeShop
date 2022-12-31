using SafeShop.Service.DTO.User;

namespace SafeShop.Service.Infrastructure
{
    public interface IUserService
    {
        Task<UserGetDTO> GetUserAsync(Guid id);
        Task PostUserAsync(UserPostDTO user);
        Task PutUserAsync(UserPutDTO user, Guid id);
        Task DeleteUserAsync(Guid id);
    }
}
