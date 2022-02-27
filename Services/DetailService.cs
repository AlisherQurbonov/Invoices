using invoice.Data;
using invoice.Entities;
using Microsoft.EntityFrameworkCore;

namespace invoice.Services;

public class DetailService : IDetailService
{

    private readonly InvoiceDbContext _ctx;
    private readonly ILogger<DetailService> _log;


    public DetailService(ILogger<DetailService> logger, InvoiceDbContext context)
    {
        _ctx = context;
        _log = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception, Detail  Detail)> CreateAsync( Detail detail)
    {
        try
        {
            await _ctx.Details.AddAsync(detail);

            await _ctx.SaveChangesAsync();


            _log.LogInformation($" Detail create in DB: {detail}");


            return (true, null, detail);
        }

        catch (Exception e)
        {
            _log.LogInformation($"Create detail to DB failed: {e.Message}", e);


            return (false, e, null);
        }

    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Detail detail)
    {
        try
        {

                _ctx.Details.Remove(detail);


                await _ctx.SaveChangesAsync();


                _log.LogInformation($"Detail remove in DB: {detail}");

                 return (true, null);
        }

        catch (Exception e)
        {
            _log.LogInformation($"Remove detail to DB failed: {e.Message}", e);


            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(int id)
      => _ctx.Details
    .AnyAsync(p => p.Id == id);

   
    public Task<List<Detail>> GetAllAsync()
    => _ctx.Details

        .AsNoTracking()

        .ToListAsync();
 
 
    public Task<List<Detail>> GetIdAsync(int id)
   => _ctx.Details

       .AsNoTracking()

       .ToListAsync();
   
  
    public Task<Detail> GetAsync(int id)
    => _ctx.Details.FirstOrDefaultAsync(a => a.Id == id);


  
    public async Task<(bool IsSuccess, Exception Exception, Detail Detail)> UpdatePostAsync(Detail detail)
    {
        try
        {
            if (await _ctx.Details.AnyAsync(t => t.Id == detail.Id))
            {
                _ctx.Details.Update(detail);

                await _ctx.SaveChangesAsync();


                _log.LogInformation($" Detail update in DB: {detail}");


                return (true, null, detail);
            }
            else
            {

                return (false, new Exception($"Detail with given ID: {detail.Id} doesnt exist!"), null);
            }
        }
       
        catch (Exception e)
        {
            _log.LogInformation($"Update detail to DB failed: {e.Message}", e);
            

            return (false, e, null);
        }
    }

}