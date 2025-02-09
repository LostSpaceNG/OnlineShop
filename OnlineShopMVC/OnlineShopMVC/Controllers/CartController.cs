using Microsoft.AspNetCore.Mvc;
using OnlineShopMVC.Helpers;
using OnlineShopMVC.Models;
using OnlineShopMVC.Services.Interfaces;
using System.Security.Claims;

namespace OnlineShopMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        // Display the cart
        public async Task<IActionResult> Index()
        {
            IEnumerable<CartItem> cartItems;

            if (User.Identity!.IsAuthenticated)
            {
                // Logged-in: retrieve from database
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cartItems = await _cartService.GetCartItemsByUserIdAsync(userId!);
            }
            else
            {
                // Guest: retrieve from session
                cartItems = SessionCartHelper.GetCartItems(HttpContext.Session);
            }

            return View(cartItems);
        }

        // Add an item to the cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            if (quantity < 1)
                quantity = 1;

            var product = await _productService.GetProductByIdAsync(productId);

            if (User.Identity!.IsAuthenticated)
            {
                // Logged-in: store in the database
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _cartService.AddToCartAsync(userId!, productId, product, quantity);
            }
            else
            {
                // Guest: store in session
                SessionCartHelper.AddToCart(HttpContext.Session, productId, product, quantity);
            }

            return RedirectToAction("Index");
        }

        // Update the quantity of an item in the cart
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
        {
            if (quantity < 1)
                quantity = 1;

            if (User.Identity!.IsAuthenticated)
            {
                // Logged-in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _cartService.UpdateCartQuantityAsync(userId!, productId, quantity);
            }
            else
            {
                // Guest
                SessionCartHelper.UpdateCartQuantity(HttpContext.Session, productId, quantity);
            }

            return RedirectToAction("Index");
        }

        // Remove an item from the cart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            if (User.Identity!.IsAuthenticated)
            {
                // Logged-in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _cartService.RemoveFromCartAsync(userId!, productId);
            }
            else
            {
                // Guest
                SessionCartHelper.RemoveFromCart(HttpContext.Session, productId);
            }

            return RedirectToAction("Index");
        }
    }
}
