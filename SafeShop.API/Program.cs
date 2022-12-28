using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SafeShop.Repository.DataAccess;
using SafeShop.Repository.Encryption;
using SafeShop.Repository.Implementation;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.DTO.Auth;
using SafeShop.Service.Implementation;
using SafeShop.Service.Infrastructure;
using Stripe;
using System.Configuration;
using System.Security.Claims;
using System.Text;

namespace SafeShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<SafeShopContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });
            var configuration = builder.Configuration;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                        (configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ResourceClaim", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        object resourceIdObject = new HttpContextAccessor().HttpContext.Request.RouteValues["id"];
                        Claim claimIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "ID");
                        if(resourceIdObject == null || claimIdClaim == null)
                        {
                            return false;
                        }

                        string resourceId = resourceIdObject.ToString();
                        string claimId = claimIdClaim.Value;
                        if(resourceId == claimId)
                        {
                            return true;
                        }
                        return false;
                    });
                });
            });

            JwtConfiguration conf = new JwtConfiguration(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"], configuration["Jwt:Key"], int.Parse(configuration["Jwt:Expire"]));
            builder.Services.AddSingleton(typeof(JwtConfiguration), conf);
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<ICartRepository, CartRepository>();
            builder.Services.AddTransient<ICartProductRepository, CartProductRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IResizerService, ResizerService>();
            builder.Services.AddTransient<IProductService, Service.Implementation.ProductService>();
            builder.Services.AddTransient<IEncryptionService, EncryptionService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ICartService, CartService>();
            builder.Services.AddTransient<ICartProductService, CartProductService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}