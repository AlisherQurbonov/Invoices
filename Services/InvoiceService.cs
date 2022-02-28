using invoice.Entities;
using invoice.Data;
using Microsoft.EntityFrameworkCore;

namespace invoice.Services;

public class InvoiceService : IInvoiceService
{

    private readonly InvoiceDbContext _ctx;
    private readonly ILogger<InvoiceService> _log;


    public InvoiceService(ILogger<InvoiceService> logger, InvoiceDbContext context)
    {
        _ctx = context;
        _log = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception, Invoice Invoice)> CreateAsync(Invoice invoice)
    {
        try
        {
            await _ctx.Invoices.AddAsync(invoice);

            await _ctx.SaveChangesAsync();


            _log.LogInformation($" Invoice create in DB: {invoice}");


            return (true, null, invoice);
        }
      
        catch (Exception e)
        {
            _log.LogInformation($"Create invoice to DB failed: {e.Message}", e);

            return (false, e, null);
        }

    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Invoice invoice)
    {
        try
        {

                _ctx.Invoices.Remove(invoice);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($"Invoice remove in DB: {invoice}");


                 return (true, null);
        }

        catch (Exception e)
        {
            _log.LogInformation($"Remove invoice to DB failed: {e.Message}", e);


            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(int id)
      => _ctx.Invoices
    .AnyAsync(p => p.Id == id);

    
    public Task<List<Invoice>> GetAllAsync()
    => _ctx.Invoices

        .AsNoTracking()

        .ToListAsync();
    
    
    public Task<List<Invoice>> GetIdAsync(int id)
   => _ctx.Invoices
       .ToListAsync();

    
    public Task<List<Invoice>> GetByIdAsync(int id)
        => _ctx.Invoices
            .ToListAsync();
  
  
    public Task<Invoice> GetAsync(int id)
    => _ctx.Invoices.FirstOrDefaultAsync(a => a.Id == id);


    public async Task<(bool IsSuccess, Exception Exception, Invoice Invoice)> UpdatePostAsync(Invoice invoice)
    {
        try
        {
            if (await _ctx.Invoices.AnyAsync(t => t.Id == invoice.Id))
            {
                _ctx.Invoices.Update(invoice);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($" Invoice update in DB: {invoice}");
                

                return (true, null, invoice);
            }
           
            else
            {

                return (false, new Exception($"Invoice with given ID: {invoice.Id} doesnt exist!"), null);
            }
        }
        catch (Exception e)
        {
            _log.LogInformation($"Update invoice to DB failed: {e.Message}", e);

            return (false, e, null);
        }
    }

}