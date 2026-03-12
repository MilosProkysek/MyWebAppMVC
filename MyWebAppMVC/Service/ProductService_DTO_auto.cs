using AutoMapper;
using MyWebAppMVC.Contracts.ViewModels.Category;
using MyWebAppMVC.Contracts.ViewModels.Products;
using MyWebAppMVC.Contracts.ViewModels.Supplier;
using MyWebAppMVC.Models;
using MyWebAppMVC.Repository;

namespace MyWebAppMVC.Service
{
    public class ProductServiceAutomapper(IProductRepository repo, IMapper mapper) : IProductServiceDTO
    {
        // ── Queries ───────────────────────────────────────────────────────────

        public IEnumerable<ProductListDto> GetAllWithDetails()
            => mapper.Map<IEnumerable<ProductListDto>>(repo.GetAllWithDetails());

        public ProductDetailDto? GetByIdWithDetails(int id)
        {
            var product = repo.GetByIdWithDetails(id);
            return product is null ? null : mapper.Map<ProductDetailDto>(product);
        }

        public IEnumerable<ProductListDto> GetByCategory(int categoryId)
            => mapper.Map<IEnumerable<ProductListDto>>(repo.GetByCategory(categoryId));

        public IEnumerable<ProductListDto> GetBySupplier(int supplierId)
            => mapper.Map<IEnumerable<ProductListDto>>(repo.GetBySupplier(supplierId));

        public IEnumerable<ProductListDto> GetActive()
            => mapper.Map<IEnumerable<ProductListDto>>(repo.GetActive());

        // ── Commands ──────────────────────────────────────────────────────────

        public ProductDetailDto Create(ProductCreateDto dto)
        {
            var entity = mapper.Map<Product>(dto);
            repo.Add(entity);
            // Reload with navigation props so the returned DTO is fully populated
            return mapper.Map<ProductDetailDto>(repo.GetByIdWithDetails(entity.Id)!);
        }

        public void Update(int id, ProductUpdateDto dto)
        {
            var entity = repo.GetById(id)
                ?? throw new KeyNotFoundException($"Product {id} not found.");

            mapper.Map(dto, entity); // maps onto the tracked entity — preserves Id, InternalNote, RowVersion
            repo.Update(entity);
        }

        public void Delete(int id)
            => repo.Delete(id);

        public bool Exists(int id)
            => repo.Exists(id);
    }

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
                .ForMember(d => d.Id,           o => o.Ignore())
                .ForMember(d => d.InternalNote, o => o.Ignore())
                .ForMember(d => d.RowVersion,   o => o.Ignore())
                .ForMember(d => d.Category,     o => o.Ignore())
                .ForMember(d => d.Supplier,     o => o.Ignore())
                .ForMember(d => d.IsActive,     o => o.MapFrom(_ => true));

            // ProductUpdateDto → Product  (map onto existing entity)
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(d => d.Id,           o => o.Ignore())
                .ForMember(d => d.InternalNote, o => o.Ignore())
                .ForMember(d => d.RowVersion,   o => o.Ignore())
                .ForMember(d => d.Category,     o => o.Ignore())
                .ForMember(d => d.Supplier,     o => o.Ignore());
        }
    }
}