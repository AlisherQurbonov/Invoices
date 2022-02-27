using invoice.Entities;

namespace invoice.Services;

public interface ICustomerService
{
     Task<(bool IsSuccess, Exception Exception, Customer Customer)> CreateAsync(Customer customer);

    Task<List<Customer>> GetAllAsync();

    Task<List<Customer>> GetIdAsync(int id);

    Task<Customer> GetAsync(int id);

    Task<(bool IsSuccess, Exception Exception, Customer Customer)> UpdatePostAsync(Customer customer);

    Task<bool> ExistsAsync(int id);

    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Customer customer);
}