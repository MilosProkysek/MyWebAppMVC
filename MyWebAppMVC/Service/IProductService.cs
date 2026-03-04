using MyWebAppMVC.Models;

namespace MyWebAppMVC.Service
{
    public interface IProductService : IGenericService<Product>
    {
        IEnumerable<Product> GetAllWithDetails();
        Product? GetByIdWithDetails(int id);
        IEnumerable<Product> GetByCategory(int categoryId);
        IEnumerable<Product> GetBySupplier(int supplierId);
        IEnumerable<Product> GetActive();
    }
}