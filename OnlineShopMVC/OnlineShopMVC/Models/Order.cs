namespace OnlineShopMVC.Models
{
    // Stores orders placed by users.
    public class Order
    {
        public int Id { get; set; }

        // FK from Identity User
        public string UserId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        // Status Options: Pending, Shipped, Delivered, Canceled
        public string Status { get; set; } = "Pending";

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
