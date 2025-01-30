namespace OnlineShopMVC.Models
{
    // Stores items within an order.
    public class OrderItem
    {
        public int Id { get; set; }

        // FK from Order
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // FK from Product
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }

        // Price at the time of order
        public decimal Price { get; set; }
    }
}
