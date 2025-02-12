using OnlineShopMVC.Models;

namespace OnlineShopMVC.ViewModels
{
    public class MyAccountViewModel
    {
        public ApplicationUser? User { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
