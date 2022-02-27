using invoice.Entities;
using invoice.Data;
using Microsoft.EntityFrameworkCore;

namespace invoice.Services;

public class PaymentService : IPaymentService
{

    private readonly InvoiceDbContext _ctx;
    private readonly ILogger<PaymentService> _log;


    public PaymentService(ILogger<PaymentService> logger, InvoiceDbContext context)
    {
        _ctx = context;
        _log = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception, Payment Payment)> CreateAsync(Payment payment)
    {
        try
        {
            await _ctx.Payments.AddAsync(payment);

            await _ctx.SaveChangesAsync();


            _log.LogInformation($" Payment create in DB: {payment}");


            return (true, null, payment);
        }
      
        catch (Exception e)
        {
            _log.LogInformation($"Create payment to DB failed: {e.Message}", e);

            return (false, e, null);
        }

    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Payment payment)
    {
        try
        {

                _ctx.Payments.Remove(payment);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($"Payment remove in DB: {payment}");


                 return (true, null);
        }

        catch (Exception e)
        {
            _log.LogInformation($"Remove payment to DB failed: {e.Message}", e);


            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(int id)
      => _ctx.Payments
    .AnyAsync(p => p.Id == id);

    
    public Task<List<Payment>> GetAllAsync()
    => _ctx.Payments

        .AsNoTracking()

        .ToListAsync();
    
    
    public Task<List<Payment>> GetIdAsync(int id)
   => _ctx.Payments

       .AsNoTracking()

       .ToListAsync();
  
  
    public Task<Payment> GetAsync(int id)
    => _ctx.Payments.FirstOrDefaultAsync(a => a.Id == id);


    public async Task<(bool IsSuccess, Exception Exception, Payment Payment)> UpdatePostAsync(Payment payment)
    {
        try
        {
            if (await _ctx.Payments.AnyAsync(t => t.Id == payment.Id))
            {
                _ctx.Payments.Update(payment);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($" Payment update in DB: {payment}");
                

                return (true, null, payment);
            }
           
            else
            {

                return (false, new Exception($"Payment with given ID: {payment.Id} doesnt exist!"), null);
            }
        }
        catch (Exception e)
        {
            _log.LogInformation($"Update payment to DB failed: {e.Message}", e);

            return (false, e, null);
        }
    }

}