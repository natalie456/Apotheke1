using System.ComponentModel.DataAnnotations;

namespace Apotheke1.Entity.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обовʼязкова")]
        [StringLength(150, ErrorMessage = "Назва не може перевищувати 150 символів")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Ціна обовʼязкова")]
        [Range(1, 10000, ErrorMessage = "Ціна має бути більшою за 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Вкажіть кількість")]
        [Range(0, 100000, ErrorMessage = "Кількість не може бути відʼємною")]
        public int Stock { get; set; }

        // навігаційні властивості
        [Required(ErrorMessage = "Оберіть категорію")]
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [Required(ErrorMessage = "Оберіть постачальника")]
        [Display(Name = "Постачальник")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        // додаткові
        [StringLength(500, ErrorMessage = "Опис не може бути довшим за 500 символів")]
        public string? Description { get; set; }
    }
}
