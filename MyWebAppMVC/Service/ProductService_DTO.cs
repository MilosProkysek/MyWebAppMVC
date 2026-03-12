using AutoMapper;
using MyWebAppMVC.Contracts.ViewModels.Category;
using MyWebAppMVC.Contracts.ViewModels.Products;
using MyWebAppMVC.Contracts.ViewModels.Supplier;
using MyWebAppMVC.Models;
using MyWebAppMVC.Repository;

using AutoMapper;

namespace MyWebAppMVC.Service
{
    public class ProductServiceDTO(IProductRepository repository) : IProductServiceDTO
    {
        // GET all — list view
        public IEnumerable<ProductListDto> GetAllWithDetails()
            => repository.GetAllWithDetails()
                         .Select(p => p.ToListDto());

        // GET one — detail view
        public ProductDetailDto? GetByIdWithDetails(int id)
            => repository.GetByIdWithDetails(id)?.ToDetailDto();

        // Filtered queries
        public IEnumerable<ProductListDto> GetByCategory(int categoryId)
            => repository.GetByCategory(categoryId)
                         .Select(p => p.ToListDto());

        public IEnumerable<ProductListDto> GetBySupplier(int supplierId)
            => repository.GetBySupplier(supplierId)
                         .Select(p => p.ToListDto());

        public IEnumerable<ProductListDto> GetActive()
            => repository.GetActive()
                         .Select(p => p.ToListDto());

        // CREATE — maps DTO → entity, persists, returns detail DTO
        public ProductDetailDto Create(ProductCreateDto dto)
        {
            var entity = dto.ToEntity();
            repository.Add(entity);
            // Reload with navigation properties so the DTO is fully populated
            return repository.GetByIdWithDetails(entity.Id)!.ToDetailDto();
        }

        // UPDATE — loads entity, applies changes, persists
        public void Update(int id, ProductUpdateDto dto)
        {
            var entity = repository.GetById(id)
                ?? throw new KeyNotFoundException($"Product {id} not found.");

            entity.ApplyUpdate(dto);
            repository.Update(entity);
        }

        // DELETE / EXISTS — delegate to repository directly
        public void Delete(int id)
            => repository.Delete(id);

        public bool Exists(int id)
            => repository.Exists(id);
    }

    // ── Mapping extensions ────────────────────────────────────────────────────

    public static class ProductMappings
    {
        public static ProductListDto ToListDto(this Product p)
            => new(
                p.Id,
                p.Name,
                p.Price,
                p.Category?.Name ?? "(no category)",
                p.Supplier?.Name ?? "(no supplier)"
            );

        public static ProductDetailDto ToDetailDto(this Product p)
            => new(
                p.Id,
                p.Name,
                p.Price,
                p.IsActive,
                new CategoryDto(p.CategoryId, p.Category?.Name ?? "(no category)"),
                new SupplierDto(p.SupplierId, p.Supplier?.Name ?? "(no supplier)", p.Supplier?.Country)
            );

        public static Product ToEntity(this ProductCreateDto dto)
            => new()
            {
                Name = dto.Name.Trim(),
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId,
                IsActive = true
            };

        public static void ApplyUpdate(this Product entity, ProductUpdateDto dto)
        {
            entity.Name = dto.Name.Trim();
            entity.Price = dto.Price;
            entity.IsActive = dto.IsActive;
            entity.CategoryId = dto.CategoryId;
            entity.SupplierId = dto.SupplierId;
        }
    }   
}