using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopMVC.Models;

namespace OnlineShopMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define DbSets for each model
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // this method is like a blueprint, telling EF Core how to structure the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // important for ASP.NET Identity Tables
            base.OnModelCreating(modelBuilder);

            // Fluent API to set relationships
            // first relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)             // each OrderItem is linked to one Order
                .WithMany(o => o.OrderItems)        // each Order has many OrderItems
                .HasForeignKey(oi => oi.OrderId);   // the OrderItem table has a FK pointing to Order

            // second relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)               // each OrderItem is linked to one Product
                .WithMany()                             // a Product can be in many OrderItems
                .HasForeignKey(oi => oi.ProductId);     // the OrderItem table has a FK pointing to Product
        }
    }
}
