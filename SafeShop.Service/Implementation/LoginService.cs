using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SafeShop.Core.Model;
using SafeShop.Repository.Encryption;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.Auth;
using SafeShop.Service.DTO.User;
using SafeShop.Service.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SafeShop.Service.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncryptionService encryptionService;
        private readonly IMapper mapper;
        private readonly JwtConfiguration configuration;

        public LoginService(IUserRepository userRepository, IEncryptionService encryptionService,
            IMapper mapper, JwtConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.encryptionService = encryptionService;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<UserGetDTO> LoginAsync(LoginCredentials credentials)
        {
            byte[] salt = await userRepository.GetSaltAsync(credentials.Login);
            if(salt == null)
            {
                throw new InvalidCredentialsException();
            }
            byte[] hashedPass = encryptionService.HashPasswordWithPreGeneratedSalt(credentials.Password, salt);
            UserGetDTO user = mapper.Map<User, UserGetDTO>
                (await userRepository.VerifyCredentialsAsync(credentials.Login, hashedPass));
            if(user == null)
            {
                throw new InvalidCredentialsException();
            }
            return user;
        }

        public string GenerateJWT(UserGetDTO user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("ID", user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(configuration.Issuer, configuration.Audience, claims,
                expires: DateTime.Now.AddMinutes(configuration.Expire), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public bool ValidateJWT(string token)
        {
            if(token == null)
            {
                return false;
            }

            var mySecret = Encoding.UTF8.GetBytes(configuration.Key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration.Issuer,
                    ValidAudience = configuration.Audience,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
