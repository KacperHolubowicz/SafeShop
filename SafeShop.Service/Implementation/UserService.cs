using AutoMapper;
using Microsoft.Extensions.Logging;
using SafeShop.Core.Model;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.User;
using SafeShop.Service.Exceptions;
using SafeShop.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncryptionService encryptionService;
        private readonly IMapper mapper;
        private readonly ILogger<UserService> logger;

        public UserService(IUserRepository userRepository, IEncryptionService encryptionService,
            IMapper mapper, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.encryptionService = encryptionService;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<UserGetDTO> FindUserAsync(Guid id)
        {
            User user = await userRepository.FindUserAsync(id);
            if(user == null)
            {
                throw new ResourceNotFoundException();
            }
            return mapper.Map<UserGetDTO>(user);
        }

        public async Task AddUserAsync(UserPostDTO user)
        {
            User userEntity = mapper.Map<User>(user);
            Tuple<byte[], byte[]> passwordWithSalt = encryptionService.HashPasswordWithoutPreGeneratedSalt(user.Password);
            userEntity.Password = passwordWithSalt.Item1;
            userEntity.Salt = passwordWithSalt.Item2;
            try
            {
                await userRepository.AddUserAsync(userEntity);
            } catch(Exception ex)
            {
                throw new AddingResourceException(ex.Message);
            }
        }

        public async Task UpdateUserAsync(UserPutDTO user, Guid id)
        {
            User userEntity = mapper.Map<User>(user);
            try
            {
                await userRepository.UpdateUserAsync(userEntity, id);
            } catch(Exception ex)
            {
                throw new UpdatingResourceException(ex.Message);
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
