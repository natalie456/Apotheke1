using Apotheke1.Entity.Models;
using Apotheke1.Interfaces;
using Apotheke1.Services;
using Microsoft.EntityFrameworkCore;
using Apotheke1.Data;


namespace Apotheke1.Services
{
    public class MedicineService : IMedicineService
    {
        
        private readonly ApothekeDbContext _db;

        public MedicineService(ApothekeDbContext db)
        {
            _db = db;
        }
        public IQueryable<Medicine> Query() =>  _db.Medicines.AsQueryable();
        

        public async Task<List<Medicine>> GetAllAsync() =>
            await _db.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .ToListAsync();

        public async Task<Medicine?> GetByIdAsync(int id) =>
            await _db.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

        public async Task CreateAsync(Medicine medicine)
        {
            _db.Medicines.Add(medicine);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Medicine medicine)
        {
            _db.Medicines.Update(medicine);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var medicine = await _db.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _db.Medicines.Remove(medicine);
                await _db.SaveChangesAsync();
            }
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            return await _db.Suppliers.ToListAsync();
        }

        public async Task<PagedResult<Medicine>> GetPagedAsync(
    string? search,
    string? sortOrder,
    bool ascending,
    int pageNumber,
    int pageSize)
        {
            var query = _db.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(search))
            {
                string normalized = search.Trim().ToLower();
                query = query.Where(m => m.Name.ToLower().Contains(normalized));
            }

            
            query = sortOrder switch
            {
                "price_asc" => query.OrderBy(m => m.Price),
                "price_desc" => query.OrderByDescending(m => m.Price),
                "name_asc" => query.OrderBy(m => m.Name),
                "name_desc" => query.OrderByDescending(m => m.Name),
                _ => query.OrderBy(m => m.Id)
            };

            
            int totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            
            return new PagedResult<Medicine>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
