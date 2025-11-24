namespace Apotheke1.Entity.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;


        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; } = null!;


        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
