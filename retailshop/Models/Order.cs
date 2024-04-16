using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace retailshop.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [Required ]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Boolean IsCancel { get; set; }

       public virtual Customer Customer { get; set; }
       public virtual Product Product { get; set; }
    }
}
