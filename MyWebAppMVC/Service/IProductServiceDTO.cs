using MyWebAppMVC.Contracts.ViewModels.Products;

namespace MyWebAppMVC.Service
{
    public interface IProductServiceDTO
    {
        // Queries — return DTOs
        IEnumerable<ProductListDto> GetAllWithDetails();
        ProductDetailDto? GetByIdWithDetails(int id);
        IEnumerable<ProductListDto> GetByCategory(int categoryId);
        IEnumerable<ProductListDto> GetBySupplier(int supplierId);
        IEnumerable<ProductListDto> GetActive();

        // Commands — accept DTOs, hide the entity
        ProductDetailDto Create(ProductCreateDto dto);
        void Update(int id, ProductUpdateDto dto);
        void Delete(int id);
        bool Exists(int id);
    }
}