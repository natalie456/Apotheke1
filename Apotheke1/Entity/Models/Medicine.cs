using System.ComponentModel.DataAnnotations;

namespace Apotheke1.Entity.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Name { get; set; } = null!;


        [Required]
        public decimal Price { get; set; }


        public int Stock { get; set; }


        // навігаційні властивості
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;


        [Display(Name = "Постачальник")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;


        // додаткові
        public string? Description { get; set; }
    }
}
