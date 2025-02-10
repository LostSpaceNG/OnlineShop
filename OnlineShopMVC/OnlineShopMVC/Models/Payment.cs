namespace OnlineShopMVC.Models
{
    // Tracks payment transactions.
    public class Payment
    {
        public int Id { get; set; }

        // FK from Order
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public bool IsSuccessful { get; set; }
    }
}
