using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineShopMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public List<Order>? OrderHistory { get; set; }
        public List<Product>? FavoriteProducts { get; set; }
    }
}
