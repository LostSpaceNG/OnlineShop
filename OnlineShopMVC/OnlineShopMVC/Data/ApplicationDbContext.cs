using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopMVC.Models;

namespace OnlineShopMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Books", Description = "Philosophy and academic Books" },
                new Category { Id = 2, Name = "Electronics", Description = "Devices and gadgets" },
                new Category { Id = 3, Name = "Clothing", Description = "Men's & Women's fashion" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Powerful laptop for gaming and work", Price = 1259.99M, StockQuantity = 30, ImageUrl = "/images/laptop.jpg", CategoryId = 2 },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model with integrated AI Assistant", Price = 699.99M, StockQuantity = 50, ImageUrl = "/images/phone.jpg", CategoryId = 2 },
                new Product { Id = 3, Name = "Meditations", Description = "Reflect upon different aspects of life with Marcus Aurelius", Price = 19.99M, StockQuantity = 25, ImageUrl = "/images/meditationsBook.jpg", CategoryId = 1 },
                new Product { Id = 4, Name = "Programming Book", Description = "Learn C# with this in-depth guide", Price = 29.99M, StockQuantity = 75, ImageUrl = "/images/cSharpBook.jpg", CategoryId = 1 },
                new Product { Id = 5, Name = "T-Shirt", Description = "100% cotton t-shirt for both men and women", Price = 14.99M, StockQuantity = 100, ImageUrl = "/images/tShirt.jpg", CategoryId = 3 }
            );
        }
    }
}
