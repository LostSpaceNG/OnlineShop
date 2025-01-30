namespace OnlineShopMVC.Models
{
    // Stores products added to the shopping cart.
    public class CartItem
    {
        public int Id { get; set; }
        
        // Foreign Key from Identity User
        public string UserId { get; set; } = string.Empty;

        // FK from Product
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
