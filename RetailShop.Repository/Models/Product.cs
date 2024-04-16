using System.ComponentModel.DataAnnotations;

namespace retailshop.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; } 

        [Required]
        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public Boolean IsActive { get; set; }

    }
}
