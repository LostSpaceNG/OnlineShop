using System.ComponentModel.DataAnnotations;

namespace OnlineShopMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string[]? Media { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
