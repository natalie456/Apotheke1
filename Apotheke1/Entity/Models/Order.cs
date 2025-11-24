using System.ComponentModel.DataAnnotations.Schema;

namespace Apotheke1.Entity.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;


        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();



        public decimal Total { get; set; }
    }
}
