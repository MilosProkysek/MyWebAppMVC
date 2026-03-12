namespace MyWebAppMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        // Interní pole – nechceme ven
        public string? InternalNote { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = default!;

        // Concurrency (ukázka, proč entity nevracet přímo)
        public byte[]? RowVersion { get; set; }
    }
}
