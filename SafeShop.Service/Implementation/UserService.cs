using AutoMapper;
using Microsoft.Extensions.Logging;
using SafeShop.Core.Model;
using SafeShop.Repository.Encryption;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.User;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;

namespace SafeShop.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncryptionService encryptionService;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IEncryptionService encryptionService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.encryptionService = encryptionService;
            this.mapper = mapper;
        }

        public async Task<UserGetDTO> GetUserAsync(Guid id)
        {
            User user = await userRepository.FindUserAsync(id);
            if(user == null)
            {
                throw new ResourceNotFoundException();
            }
            return mapper.Map<UserGetDTO>(user);
        }

        public async Task PostUserAsync(UserPostDTO user)
        {
            User userEntity = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Login,
                Role = "User"
            };
            Tuple<byte[], byte[]> passwordWithSalt = encryptionService.HashPasswordWithoutPreGeneratedSalt(user.Password);
            userEntity.Password = passwordWithSalt.Item1;
            userEntity.Salt = passwordWithSalt.Item2;
            try
            {
                await userRepository.AddUserAsync(userEntity);
            } catch(ArgumentException ex)
            {
                throw new DuplicatedValueException(ex.Message);
            } catch(Exception ex2)
            {
                throw new AddingResourceException(ex2.Message);
            }
        }

        public async Task PutUserAsync(UserPutDTO user, Guid id)
        {
            User userEntity = mapper.Map<User>(user);
            try
            {
                await userRepository.UpdateUserAsync(userEntity, id);
            } catch(ArgumentException ex)
            {
                throw new DuplicatedValueException(ex.Message);
            } 
            catch(Exception ex2)
            {
                throw new UpdatingResourceException(ex2.Message);
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                await userRepository.DeleteUserAsync(id);
            } catch(Exception ex)
            {
                throw new ResourceNotFoundException(ex.Message);
            }
        }
    }
}
