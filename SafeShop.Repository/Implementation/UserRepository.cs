using SafeShop.Core.Model;
using SafeShop.Repository.DataAccess;
using SafeShop.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly SafeShopContext context;

        public UserRepository(SafeShopContext context)
        {
            this.context = context;
        }

        public async Task<User> FindUserAsync(Guid id)
        {
            User user = await context.Users.FindAsync(id);
            return user;
        }

        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            User oldUser = await context.Users.FindAsync(user.ID);
            if(oldUser == null)
            {
                throw new NullReferenceException("No such resource to update");
            }
            oldUser.FirstName = user.FirstName ?? oldUser.FirstName;
            oldUser.LastName = user.LastName ?? oldUser.LastName;
            oldUser.Email = user.Email ?? oldUser.Email;
            context.Users.Update(oldUser);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            User user = await context.Users.FindAsync(id);
            if(user != null)
            {
                context.Users.Remove(user);
            }
        }
    }
}
