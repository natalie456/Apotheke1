using Apotheke1.Entity.Models;
using Apotheke1.Interfaces;
using Apotheke1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Apotheke1.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineService _service;

        public MedicineController(IMedicineService service)
        {
            _service = service;
        }

        // ------------------ INDEX ------------------
        // ------------------ INDEX ------------------
        public async Task<IActionResult> Index(string? search, string? sortOrder)
        {
            // Завжди завантажуйте Категорії та Постачальників для фільтрів у ViewBags
            ViewBag.Categories = await _service.GetCategoriesAsync();
            ViewBag.Suppliers = await _service.GetSuppliersAsync();

            // Передача параметрів пошуку
            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;

            // Оскільки ви використовуєте пагінацію, краще викликати GetPagedAsync
            // Але якщо ви тимчасово використовуєте GetAllAsync, то...
            var medicines = await _service.GetAllAsync();

            // Переконайтеся, що ви обробили випадок, коли medicines може бути null, 
            // хоча ваш сервіс, ймовірно, повертає порожній List<Medicine>

            return View(medicines);
        }

        // ------------------ CREATE GET ------------------
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name");
            ViewBag.Suppliers = new SelectList(await _service.GetSuppliersAsync(), "Id", "Name");

            return View();
        }

        // ------------------ CREATE POST ------------------
        [HttpPost]
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

        // ------------------ EDIT GET ------------------
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var medicine = await _service.GetByIdAsync(id);
            if (medicine == null) return NotFound();

            ViewBag.Categories = new SelectList(await _service.GetCategoriesAsync(), "Id", "Name", medicine.CategoryId);
            ViewBag.Suppliers = new SelectList(await _service.GetSuppliersAsync(), "Id", "Name", medicine.SupplierId);

            return View(medicine);
        }

        // ------------------ EDIT POST ------------------
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

        // ------------------ DELETE ------------------
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}