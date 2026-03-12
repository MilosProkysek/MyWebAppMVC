using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebAppMVC.Contracts.ViewModels.Products;
using MyWebAppMVC.Models;
using MyWebAppMVC.Repository;
using MyWebAppMVC.Service;

namespace MyWebAppMVC.Controllers
{
    public class ProductsXController(
        IProductServiceDTO productService,
        IGenericService<Category> categoryService,
        IGenericService<Supplier> supplierService,
        IProductRepository productRepository) : Controller
    {
        // GET: ProductsX
        public IActionResult Index()
        {
            var products = from p in productRepository.GetAllWithDetails()
                                            select new ProductListDto
                                            (
                                                p.Id,
                                                p.Name,
                                                p.Price,
                                                p.Category?.Name ?? "(no category)",
                                                p.Supplier?.Name ?? "(no supplier)"
                                            );

            return View(products);


            return View(productService.GetAllWithDetails());
        }


        // GET: ProductsX/Details/5
        public IActionResult Details(int? id)
        {
            if (id is null) return NotFound();

            var product = productService.GetByIdWithDetails(id.Value);
            if (product is null) return NotFound();

            return View(product);
        }

        // GET: ProductsX/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: ProductsX/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(dto.CategoryId, dto.SupplierId);
                return View(dto);
            }

            productService.Create(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductsX/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id is null) return NotFound();

            var product = productService.GetByIdWithDetails(id.Value);
            if (product is null) return NotFound();

            PopulateDropdowns(product.Category.Id, product.Supplier.Id);
            return View(product);
        }

        // POST: ProductsX/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(dto.CategoryId, dto.SupplierId);
                return View(dto);
            }

            try
            {
                productService.Update(id, dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productService.Exists(id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProductsX/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();

            var product = productService.GetByIdWithDetails(id.Value);
            if (product is null) return NotFound();

            return View(product);
        }

        // POST: ProductsX/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private void PopulateDropdowns(int? selectedCategory = null, int? selectedSupplier = null)
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "Id", "Name", selectedCategory);
            ViewData["SupplierId"] = new SelectList(supplierService.GetAll(), "Id", "Name", selectedSupplier);
        }
    }
}
