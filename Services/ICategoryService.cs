using invoice.Entities;

namespace invoice.Services;

public interface ICategoryService
{
    Task<(bool IsSuccess, Exception Exception, Category Category)> CreateAsync(Category category);
    Task<List<Category>> GetAllAsync();

    Task<List<Category>> GetIdAsync(int id);
    Task<Category> GetAsync(int id);
    Task<(bool IsSuccess, Exception Exception, Category Category)> UpdatePostAsync(Category category);
    Task<bool> ExistsAsync(int id);
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Category category);
}