using MyWebAppMVC.Models;

namespace MyWebAppMVC.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetAllWithDetails();
        Product? GetByIdWithDetails(int id);
        IEnumerable<Product> GetByCategory(int categoryId);
        IEnumerable<Product> GetBySupplier(int supplierId);
        IEnumerable<Product> GetActive();
    }
}