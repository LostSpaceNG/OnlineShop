using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopMVC.Data;
using OnlineShopMVC.Helpers;
using OnlineShopMVC.Models;
using OnlineShopMVC.Services;
using OnlineShopMVC.Services.Interfaces;
using OnlineShopMVC.ViewModels;
using System.Security.Claims;

namespace OnlineShopMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;

        public OrderController(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        // GET: /Order/Checkout
        public IActionResult Checkout()
        {
            // Display the checkout form
            return View(new CheckoutViewModel());
        }

        // POST: /Order/Checkout
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Get cart items based on authentication
            IEnumerable<CartItem> cartItems;

            if (User.Identity!.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cartItems = await _cartService.GetCartItemsByUserIdAsync(userId!);
            }
            else
            {
                cartItems = SessionCartHelper.GetCartItems(HttpContext.Session);
            }

            if (cartItems == null || !cartItems.Any())
            {
                ModelState.AddModelError("", "Your cart is empty.");
                return View(model);
            }

            foreach (var item in cartItems)
            {
                // Ensure that product details are available
                if (item.Product == null)
                {
                    item.Product = (await _context.Products.FindAsync(item.ProductId))!;
                }

                // Check Stock
                if (item.Quantity > item.Product.StockQuantity)
                {
                    ModelState.AddModelError("", $"Insufficient stock for '{item.Product.Name}'. Only {item.Product.StockQuantity} available.");
                    return View(model);
                }

                // Subtract the ordered quantity from the product's stock
                item.Product.StockQuantity -= item.Quantity;

                // Update the product in the database
                _context.Products.Update(item.Product);
            }

            // Create a new order
            var order = new Order
            {
                UserId = (User.Identity!.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : $"guest_{model.FullName}_{DateTime.UtcNow}")!,
                OrderDate = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(item => item.Product != null ? item.Quantity * item.Product.Price : 0),
                ShippingAddress = model.ShippingAddress,
                City = model.City,
                PostalCode = model.PostalCode,
                Country = model.Country,
                Status = "Pending"
            };

            _context.Orders.Add(order);
            // Save to get the order ID later
            await _context.SaveChangesAsync();
            
            // Create order items for each cart item
            foreach (var item in cartItems)
            {
                // Ensure that product details are available
                if (item.Product == null)
                {
                    item.Product = (await _context.Products.FindAsync(item.ProductId))!;
                }

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                _context.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            // Simulate Payment Processing
            var payment = new Payment
            {
                OrderId = order.Id,
                Amount = order.TotalAmount,
                PaymentMethod = model.PaymentMethod,
                PaymentDate = DateTime.UtcNow,
                IsSuccessful = true     // For simulation, we mark the payment as always successful 
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Clear the cart
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _cartService.ClearCartAsync(userId!);
            }
            else
            {
                HttpContext.Session.Remove("Cart");
            }

            return RedirectToAction("Confirmation", new { orderId = order.Id });
        }

        // GET: /Order/Confirmation?orderId=123
        public async Task<IActionResult> Confirmation(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)     // Include according order items
                .ThenInclude(oi => oi.Product)  // Include product details for each order item
                .Include(o => o.Payment)    // Include payment details
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
