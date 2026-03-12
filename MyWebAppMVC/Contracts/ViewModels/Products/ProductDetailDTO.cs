using MyWebAppMVC.Contracts.ViewModels.Category;
using MyWebAppMVC.Contracts.ViewModels.Supplier;

namespace MyWebAppMVC.Contracts.ViewModels.Products
{
    public record ProductDetailDto(
        int Id,
        string Name,
        decimal Price,
        bool IsActive,
        CategoryDto Category,
        SupplierDto Supplier
    );
}
