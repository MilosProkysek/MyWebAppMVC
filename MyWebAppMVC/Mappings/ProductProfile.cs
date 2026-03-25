using AutoMapper;
using MyWebAppMVC.Contracts.ViewModels.Category;
using MyWebAppMVC.Contracts.ViewModels.Products;
using MyWebAppMVC.Contracts.ViewModels.Supplier;
using MyWebAppMVC.Models;

namespace MyWebAppMVC.Mappings
{
    // ── AutoMapper profile ────────────────────────────────────────────────────

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Product → ProductListDto  (flat projection)
            CreateMap<Product, ProductListDto>()
                .ForCtorParam("CategoryName", opt => opt.MapFrom(src => src.Category.Name ?? "(no category)"))
                .ForCtorParam("SupplierName", opt => opt.MapFrom(src => src.Supplier.Name ?? "(no supplier)"));

            // Product → ProductDetailDto  (nested DTOs)
            CreateMap<Product, ProductDetailDto>()
                .ForCtorParam("Category", opt => opt.MapFrom(src => src.Category))
                .ForCtorParam("Supplier", opt => opt.MapFrom(src => src.Supplier));

            // Navigation entities → nested DTOs  (resolved via convention)
            CreateMap<Category, CategoryDto>();
            CreateMap<Supplier, SupplierDto>();

            // ProductCreateDto → Product  (new entity)
            CreateMap<ProductCreateDto, Product>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.InternalNote, o => o.Ignore())
                .ForMember(d => d.RowVersion, o => o.Ignore())
                .ForMember(d => d.Category, o => o.Ignore())
                .ForMember(d => d.Supplier, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.MapFrom(_ => true));

            // ProductUpdateDto → Product  (map onto existing entity)
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.InternalNote, o => o.Ignore())
                .ForMember(d => d.RowVersion, o => o.Ignore())
                .ForMember(d => d.Category, o => o.Ignore())
                .ForMember(d => d.Supplier, o => o.Ignore());
        }
    }
}
