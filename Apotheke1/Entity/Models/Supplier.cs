using System.ComponentModel.DataAnnotations;

namespace Apotheke1.Entity.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }


        public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
