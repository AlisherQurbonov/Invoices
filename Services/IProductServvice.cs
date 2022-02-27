using invoice.Entities;

namespace invoice.Services;

public interface IProductService
{
     Task<(bool IsSuccess, Exception Exception, Product Product)> CreateAsync(Product product);

    Task<List<Product>> GetAllAsync();

    Task<List<Product>> GetIdAsync(int id);

    Task<Product> GetAsync(int id);

    Task<(bool IsSuccess, Exception Exception, Product Product)> UpdatePostAsync(Product product);

    Task<bool> ExistsAsync(int id);
    
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Product product);
}