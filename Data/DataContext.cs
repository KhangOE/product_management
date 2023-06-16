using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Product_management.Models;
using System.Diagnostics;

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
         
            modelBuilder.Entity<Order>()
                .HasOne(e => e.User)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false   );
        
        /*  modelBuilder.Entity<Order>()
                    .HasOne(p => p.User)
                    .WithMany(pc => pc.Orders)
                    .HasForeignKey(p => p.UserId); */




        /*  modelBuilder.Entity<User>()
                  .HasMany(g => g.Orders)
                   .WithOne(s => s.User)
                   .HasForeignKey(s => s.UserId); */




    }
    }
}
