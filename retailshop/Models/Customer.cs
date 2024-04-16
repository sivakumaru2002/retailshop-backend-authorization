using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace retailshop.Models
{
    public class Customer
    {
        [Key]
        public Guid  CustomerId { get; set; }
        [Required]
        public string? CustomerName { get; set; }

        public long Mobile { get; set; }

        public string? Email { get; set; }

    }
}