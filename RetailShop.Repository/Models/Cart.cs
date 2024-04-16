using System.ComponentModel.DataAnnotations;

namespace RetailShop.Repository.Models
{
    public class Cart
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
