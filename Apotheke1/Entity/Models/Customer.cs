using System.ComponentModel.DataAnnotations;

namespace Apotheke1.Entity.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required, StringLength(120)]
        public string FullName { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }
        public string? Phone { get; set; }


        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
