using invoice.Entities;

namespace invoice.Services;

public interface IInvoiceService
{
     Task<(bool IsSuccess, Exception Exception, Invoice Invoice)> CreateAsync(Invoice invoice);

    Task<List<Invoice>> GetAllAsync();

    Task<List<Invoice>> GetIdAsync(int id);

    Task<Invoice> GetAsync(int id);

    Task<(bool IsSuccess, Exception Exception, Invoice Invoice)> UpdatePostAsync(Invoice invoice);

    Task<bool> ExistsAsync(int id);
    
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Invoice invoice);
}