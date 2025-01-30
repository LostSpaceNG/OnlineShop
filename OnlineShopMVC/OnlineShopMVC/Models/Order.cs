namespace OnlineShopMVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product>? Products { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public User? UserData { get; set; }
    }
}
