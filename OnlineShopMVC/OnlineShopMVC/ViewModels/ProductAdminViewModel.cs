using System.ComponentModel.DataAnnotations;

namespace OnlineShopMVC.ViewModels
{
    public class ProductAdminViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;

        // Use string for price input
        [Required(ErrorMessage = "Price is required")]
        public string PriceInput { get; set; } = string.Empty;
        
        [Required]
        public int StockQuantity { get; set; }

        // Use default ImageUrl for new products (for simplicity and testing purposes)
        public string ImageUrl { get; set; } = "/images/mystery.jpg";

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }
    }
}
