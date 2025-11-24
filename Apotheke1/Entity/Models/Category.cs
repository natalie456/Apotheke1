using System.ComponentModel.DataAnnotations;

namespace Apotheke1.Entity.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;


        public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
