using System.ComponentModel.DataAnnotations;

namespace OnlineShopMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string[]? Media { get; set; }
    }
}
