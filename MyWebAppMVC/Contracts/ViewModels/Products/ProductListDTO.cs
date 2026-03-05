namespace MyWebAppMVC.Contracts.ViewModels.Products
{
    public record ProductListDto(
        int Id,
        string Name,
        decimal Price,
        string CategoryName,
        string SupplierName
     );

}
