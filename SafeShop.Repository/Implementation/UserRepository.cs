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
            if(context.Users.Any(u => u.Login == user.Login || u.Email == user.Email))
            {
                throw new ArgumentException("There is already a user with given login or email");
            }
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user, Guid id)
        {
            user.ID = id;
            User oldUser = await context.Users.FindAsync(user.ID);
            if(oldUser == null)
            {
                throw new NullReferenceException("No such resource to update");
            } else if(context.Users.Any(u => u.Email == user.Email))
            {
                throw new ArgumentException("There is already a user with given email");
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
                await context.SaveChangesAsync();
            }
        }

        public async Task<byte[]> GetSaltAsync(string login)
        {
            byte[] salt = await Task.FromResult(context.Users.FirstOrDefault(u => u.Login == login).Salt);
            return salt;
        }

        public async Task<User> VerifyCredentialsAsync(string login, byte[] password)
        {
            User user = await Task.FromResult(context.Users.FirstOrDefault(u => u.Login == login && u.Password == password));
            return user;
        }
    }
}
