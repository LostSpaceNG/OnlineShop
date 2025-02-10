using Microsoft.EntityFrameworkCore;
using OnlineShopMVC.Data;
using OnlineShopMVC.Models;
using OnlineShopMVC.Services.Interfaces;

namespace OnlineShopMVC.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)      // Include Product Details
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }

        public async Task AddToCartAsync(string userId, int productId, Product product, int quantity)
        {
            // does the cart item already exist ?
            var existingItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
            // yes, then increase quantity
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                // no, then create new cart item and add to database
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UserId = userId,
                    Product = product
                };

                await _context.CartItems.AddAsync(cartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string userId, int productId)
        {
            // find item
            var item = await _context.CartItems.FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);

            // remove item
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartQuantityAsync(string userId, int productId, int quantity)
        {
            // find item
            var item = await _context.CartItems.FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);

            // update quantity
            if (item != null)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var items = _context.CartItems.Where(ci => ci.UserId == userId);
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
