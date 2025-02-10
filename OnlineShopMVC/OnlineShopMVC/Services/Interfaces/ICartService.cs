using OnlineShopMVC.Models;

namespace OnlineShopMVC.Services.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId);
        Task AddToCartAsync(string userId, int productId, Product product, int quantity);
        Task RemoveFromCartAsync(string userId, int productId);
        Task UpdateCartQuantityAsync(string userId, int productId, int quantity);
        Task ClearCartAsync(string userId);
    }
}
