using System.ComponentModel.DataAnnotations;

namespace MyWebAppMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
