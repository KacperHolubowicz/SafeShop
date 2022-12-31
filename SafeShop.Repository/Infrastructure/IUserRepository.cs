using SafeShop.Core.Model;

namespace SafeShop.Repository.Infrastructure
{
    public interface IUserRepository
    {
        Task<User> FindUserAsync(Guid id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user, Guid id);
        Task DeleteUserAsync(Guid id);
        Task<byte[]> GetSaltAsync(string login);
        Task<User> VerifyCredentialsAsync(string login, byte[] password);
    }
}
