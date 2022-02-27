using invoice.Entities;
using invoice.Data;
using Microsoft.EntityFrameworkCore;

namespace invoice.Services;

public class CustomerService : ICustomerService
{

    private readonly InvoiceDbContext _ctx;
    private readonly ILogger<CustomerService> _log;


    public CustomerService(ILogger<CustomerService> logger, InvoiceDbContext context)
    {
        _ctx = context;
        _log = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception, Customer Customer)> CreateAsync(Customer customer)
    {
        try
        {

            await _ctx.Customeries.AddAsync(customer);


            await _ctx.SaveChangesAsync();


            _log.LogInformation($" Customer create in DB: {customer}");


            return (true, null, customer);
        }

        catch (Exception e)
        {

            _log.LogInformation($"Create customer to DB failed: {e.Message}", e);


            return (false, e, null);
        }

    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Customer customer)
    {
        try
        {

                _ctx.Customeries.Remove(customer);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($"Customer remove in DB: {customer}");


                 return (true, null);
        }

        catch (Exception e)
        {
            _log.LogInformation($"Remove customer to DB failed: {e.Message}", e);


            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(int id)
      => _ctx.Customeries
    .AnyAsync(p => p.Id == id);

   
    public Task<List<Customer>> GetAllAsync()
    => _ctx.Customeries

        .AsNoTracking()

        .Include(o=>o.Orders)

        .ToListAsync();
   
    public Task<List<Customer>> GetIdAsync(int id)
   => _ctx.Customeries

       .AsNoTracking()

       .ToListAsync();
 
    public Task<Customer> GetAsync(int id)
    => _ctx.Customeries.FirstOrDefaultAsync(a => a.Id == id);


    public async Task<(bool IsSuccess, Exception Exception, Customer Customer)> UpdatePostAsync(Customer customer)
    {
        try
        {
            if (await _ctx.Customeries.AnyAsync(t => t.Id == customer.Id))
            {
                _ctx.Customeries.Update(customer);

                await _ctx.SaveChangesAsync();


                _log.LogInformation($" Customer update in DB: {customer}");


                return (true, null, customer);
            }
           
            else
            {

                return (false, new Exception($"Customer with given ID: {customer.Id} doesnt exist!"), null);
            }
        }
       
        catch (Exception e)
        {

            _log.LogInformation($"Update customer to DB failed: {e.Message}", e);


            return (false, e, null);
        }
    }

}