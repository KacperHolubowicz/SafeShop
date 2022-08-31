using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeShop.Core.Model;

namespace SafeShop.Repository.DataAccess
{
    public class SafeShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public SafeShopContext(DbContextOptions<SafeShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p =>
            {
                p.HasKey(pr => pr.ID);
                p.Property(pr => pr.Price).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<CartProduct>(cp =>
            {
                cp.HasKey(p => p.ID);
                cp.HasOne(p => p.Product);
                cp.HasOne(p => p.Cart)
                .WithMany(c => c.Products);
                cp.Property(p => p.Total).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<Cart>(c =>
            {
                c.HasKey(p => p.ID);
                c.HasOne(ca => ca.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserID);
            });

            modelBuilder.Entity<User>()
                .HasKey(u => u.ID);

            modelBuilder.Entity<Order>(o =>
            {
                o.HasKey(or => or.ID);
                o.HasMany(or => or.Products)
                .WithOne(op => op.Order);
                o.HasOne(or => or.User)
                .WithMany(u => u.Orders);
                o.Property(or => or.Total).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<OrderProduct>(op =>
            {
                op.HasKey(opr => opr.ID);
                op.HasOne(o => o.Product);
                op.Property(opr => opr.Total).HasColumnType("decimal(8, 2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
