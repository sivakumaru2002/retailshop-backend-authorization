using System.ComponentModel.DataAnnotations;

namespace retailshop.Models
{
    public class CreateOrder
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
