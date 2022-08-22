using SafeShop.Service.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
