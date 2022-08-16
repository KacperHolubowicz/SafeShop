using Microsoft.EntityFrameworkCore;
using SafeShop.Repository.DataAccess;
using SafeShop.Repository.Implementation;
using SafeShop.Repository.Infrastructure;
using SafeShop.Service.Implementation;
using SafeShop.Service.Infrastructure;

namespace SafeShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SafeShopContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IProductService, ProductService>();
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