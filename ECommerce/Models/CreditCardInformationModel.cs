using System.ComponentModel.DataAnnotations;

namespace ECommerceBackEnd.Models
{
    public class CreditCardInformationModel
    {
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string CVV { get; set; }
        [Required]
        public string PAN { get; set; }
    }
}
