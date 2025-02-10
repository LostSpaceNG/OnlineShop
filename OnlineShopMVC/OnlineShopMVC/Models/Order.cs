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

        // Shipping details
        public string ShippingAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        // Navigation property for order items
        public List<OrderItem> OrderItems { get; set; } = new();

        // Navigation property for payment
        public Payment? Payment { get; set; }
    }
}
