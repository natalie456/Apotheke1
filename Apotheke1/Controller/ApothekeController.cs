using Apotheke1.Entity.Models;
using Apotheke1.Interfaces;
using Apotheke1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;



namespace Apotheke1.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineService _service;

        public MedicineController(IMedicineService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(
    string? search,
    int? categoryId,
    int? supplierId,
    string? sortOrder)
        {
            // Дані для фільтрів
            ViewBag.Categories = await _service.GetCategoriesAsync();
            ViewBag.Suppliers = await _service.GetSuppliersAsync();

            // Для збереження стану фільтрів
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.SupplierId = supplierId;
            ViewBag.SortOrder = sortOrder;

            // --- Отримуємо ВСІ товари ---
            var medicines = await _service.GetAllAsync();

            // --- Пошук ---
            if (!string.IsNullOrWhiteSpace(search))
            {
                medicines = medicines
                    .Where(m => m.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // --- Фільтр категорії ---
            if (categoryId.HasValue)
            {
                medicines = medicines
                    .Where(m => m.CategoryId == categoryId)
                    .ToList();
            }

            // --- Фільтр постачальника ---
            if (supplierId.HasValue)
            {
                medicines = medicines
                    .Where(m => m.SupplierId == supplierId)
                    .ToList();
            }

            // --- Сортування ---
            medicines = sortOrder switch
            {
                "name_asc" => medicines.OrderBy(m => m.Name).ToList(),
                "name_desc" => medicines.OrderByDescending(m => m.Name).ToList(),
                "price_asc" => medicines.OrderBy(m => m.Price).ToList(),
                "price_desc" => medicines.OrderByDescending(m => m.Price).ToList(),
                _ => medicines.OrderBy(m => m.Id).ToList()
            };

            return View(medicines);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Suppliers = new SelectList(await _service.GetSuppliersAsync(), "Id", "Name");

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name");
                ViewBag.Suppliers = new SelectList(await _service.GetSuppliersAsync(), "Id", "Name");
                return View(medicine);
            }

            await _service.CreateAsync(medicine);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var medicine = await _service.GetByIdAsync(id);
            if (medicine == null) return NotFound();

            ViewBag.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name", medicine.CategoryId);
            ViewBag.Suppliers = new SelectList(await _service.GetSuppliersAsync(), "Id", "Name", medicine.SupplierId);

            return View(medicine);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name");
                ViewBag.Suppliers = new SelectList(await _service.GetSuppliersAsync(), "Id", "Name");
                return View(medicine);
            }

            await _service.UpdateAsync(medicine);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}