using Newtonsoft.Json;
using OnlineShopMVC.Models;
using OnlineShopMVC.Services.Interfaces;

namespace OnlineShopMVC.Helpers
{
    public static class SessionCartHelper
    {
        private const string CartSessionKey = "Cart";

        public static void AddToCart(ISession session, int productId, Product product, int quantity)
        {
            // get cart items
            var cart = GetCartItems(session);

            // product already in cart ?
            var existingItem = cart.FirstOrDefault(ci => ci.ProductId == productId);
            // yes, then increase quantity
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                // no, then create new item and save changes
                cart.Add(new CartItem 
                { 
                    ProductId = productId, 
                    Quantity = quantity,
                    Product = product
                });
            }

            SaveCart(session, cart);
        }

        public static void UpdateCartQuantity(ISession session, int productId, int quantity)
        {
            var cart = GetCartItems(session);
            var item = cart.FirstOrDefault(ci => ci.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
            }

            SaveCart(session, cart);
        }

        public static void RemoveFromCart(ISession session, int productId)
        {
            var cart = GetCartItems(session);
            cart.RemoveAll(ci => ci.ProductId == productId);

            SaveCart(session, cart);
        }

        public static List<CartItem> GetCartItems(ISession session)
        {
            var cartJson = session.GetString(CartSessionKey);

            return string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cartJson)!;
        }

        private static void SaveCart(ISession session, List<CartItem> cart)
        {
            session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
        }
    }
}
