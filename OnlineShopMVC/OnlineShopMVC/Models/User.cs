using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string? Address { get; set; }
        public List<Order>? OrderHistory { get; set; }
        public List<Product>? FavoriteProducts { get; set; }
    }
}
