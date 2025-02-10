using System.ComponentModel.DataAnnotations;

namespace OnlineShopMVC.ViewModels
{
    // this model collects shipping details from the user
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shipping Address is required.")]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal Code is required.")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
