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

}