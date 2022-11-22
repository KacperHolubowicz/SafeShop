using SafeShop.Service.DTO.Auth;
using SafeShop.Service.DTO.User;

namespace SafeShop.Service.Infrastructure
{
    public interface ILoginService
    {
        Task<UserGetDTO> LoginAsync(LoginCredentials credentials);
        string GenerateJWT(UserGetDTO user);
        public bool ValidateJWT(string token);
    }
}
