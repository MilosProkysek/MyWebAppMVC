using System.ComponentModel.DataAnnotations;

namespace MyWebAppMVC.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;

        [MaxLength(100)]
        public string? Country { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
