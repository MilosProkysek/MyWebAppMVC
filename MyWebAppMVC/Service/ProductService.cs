using MyWebAppMVC.Models;
using MyWebAppMVC.Repository;
using MyWebAppMVC.Contracts.ViewModels.Products;
using MyWebAppMVC.Contracts.ViewModels.Category;
using MyWebAppMVC.Contracts.ViewModels.Supplier;

namespace MyWebAppMVC.Service
{
    public class ProductService(IProductRepository repository)
        : GenericService<Product>(repository), IProductService
    {
        private readonly IProductRepository _productRepository = repository;

        public IEnumerable<Product> GetAllWithDetails()
            => _productRepository.GetAllWithDetails();

        public Product? GetByIdWithDetails(int id)
            => _productRepository.GetByIdWithDetails(id);

        public IEnumerable<Product> GetByCategory(int categoryId)
            => _productRepository.GetByCategory(categoryId);

        public IEnumerable<Product> GetBySupplier(int supplierId)
            => _productRepository.GetBySupplier(supplierId);

        public IEnumerable<Product> GetActive()
            => _productRepository.GetActive();
    }
}