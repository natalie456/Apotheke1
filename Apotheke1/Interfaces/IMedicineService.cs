using Apotheke1.Entity.Models;
using Apotheke1.Data;
using Apotheke1.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Apotheke1.Interfaces
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetAllAsync();
        Task<Medicine?> GetByIdAsync(int id);
        Task<PagedResult<Medicine>> GetPagedAsync(string? search, string? sortBy, bool desc, int page, int pageSize);
        Task CreateAsync(Medicine med);
        Task UpdateAsync(Medicine med);
        Task DeleteAsync(int id);
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Supplier>> GetSuppliersAsync();
    }
}
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
}
