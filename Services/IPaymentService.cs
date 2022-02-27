using invoice.Entities;

namespace invoice.Services;

public interface IPaymentService
{
     Task<(bool IsSuccess, Exception Exception, Payment Payment)> CreateAsync(Payment payment);

    Task<List<Payment>> GetAllAsync();

    Task<List<Payment>> GetIdAsync(int id);

    Task<Payment> GetAsync(int id);

    Task<(bool IsSuccess, Exception Exception, Payment Payment)> UpdatePostAsync(Payment payment);

    Task<bool> ExistsAsync(int id);
    
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Payment payment);
}