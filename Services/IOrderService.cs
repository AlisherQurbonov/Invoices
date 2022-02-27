using invoice.Entities;

namespace invoice.Services;

public interface IOrderService
{
     Task<(bool IsSuccess, Exception Exception, Order Order)> CreateAsync(Order order);

    Task<List<Order>> GetAllAsync();

    Task<List<Order>> GetIdAsync(int id);

    Task<Order> GetAsync(int id);

    Task<(bool IsSuccess, Exception Exception, Order Order)> UpdatePostAsync(Order order);

    Task<bool> ExistsAsync(int id);
    
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Order order);
}