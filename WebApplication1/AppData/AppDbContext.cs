using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.AppData
{
    // DbContext này là cầu nối giữa Entity Framework và MySQL Database
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Cấu hình relationship giữa Category và Product (One-to-Many)
            builder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Cấu hình relationship giữa Order và OrderProduct (One-to-Many)
            builder.Entity<Order>()
                .HasMany(o => o.OrderProducts)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình relationship giữa Product và OrderProduct (One-to-Many)
            builder.Entity<Product>()
                .HasMany(p => p.OrderProducts)
                .WithOne(op => op.Product)
                .HasForeignKey(op => op.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình composite key cho OrderProduct (Many-to-Many)
            builder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            // Cấu hình UserId trong Order để liên kết với Identity User
            builder.Entity<Order>()
                .Property(o => o.UserId)
                .HasMaxLength(450); // Chiều dài tối đa cho UserId
        }
        
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    }
}
