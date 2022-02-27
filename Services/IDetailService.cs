using invoice.Entities;

namespace invoice.Services;

public interface IDetailService
{
     Task<(bool IsSuccess, Exception Exception, Detail Detail)> CreateAsync(Detail detail);
    Task<List<Detail>> GetAllAsync();

    Task<List<Detail>> GetIdAsync(int id);
    Task<Detail> GetAsync(int id);
    Task<(bool IsSuccess, Exception Exception, Detail Detail)> UpdatePostAsync(Detail detail);
    Task<bool> ExistsAsync(int id);
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Detail detail);
}