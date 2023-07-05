using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Product_management.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Xml;

namespace Product_management.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }   
        public DbSet<Cart> Carts { get; set; }  
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<OrderDetail>()
              .HasKey(pc => new { pc.OrderId, pc.ProductId });
            modelBuilder.Entity<OrderDetail>()
                       .HasOne(p => p.Order)
                       .WithMany(pc =>pc.OrderDetails)
                       .HasForeignKey(p => p.OrderId);
            modelBuilder.Entity<OrderDetail>()
                      .HasOne(p => p.Product)
                      .WithMany(pc => pc.OrderDetails)
                      .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.CategoryId, pc.ProductId });
            modelBuilder.Entity<ProductCategory>()
                       .HasOne(p => p.Category)
                       .WithMany(pc => pc.ProductCategorys)
                       .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<ProductCategory>()
                      .HasOne(p => p.Product)
                      .WithMany(pc => pc.ProductCategorys)
                      .HasForeignKey(p => p.ProductId);


            modelBuilder.Entity<Order>()
                .HasOne(e => e.User)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

            
            modelBuilder.Entity<User>()
            .Property(b => b.Adress)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<int, Address>>(v));

            modelBuilder.Entity<Product>()
            .Property(b => b.AddressesIdAvailable)
            .HasConversion(
              v => JsonConvert.SerializeObject(v),
              v => JsonConvert.DeserializeObject<HashSet<int>>(v));


            modelBuilder.Entity<User>()
           .Property(b => b.ProductsSaved)
           .HasConversion(
               v => JsonConvert.SerializeObject(v),
               v => JsonConvert.DeserializeObject<Dictionary<int, Product>>(v));

          
            //JsonConvert.DeserializeObject<Dictionary<string, string>>(v));
            //  modelBuilder.Entity<User>().Property(b => b.Adress).Metadata.SetProviderClrType(null);

        }
    }
}
