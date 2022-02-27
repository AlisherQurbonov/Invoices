using invoice.Entities;
using invoice.Data;
using Microsoft.EntityFrameworkCore;

namespace invoice.Services;

public class OrderService : IOrderService
{

    private readonly InvoiceDbContext _ctx;
    private readonly ILogger<OrderService> _log;


    public OrderService(ILogger<OrderService> logger, InvoiceDbContext context)
    {
        _ctx = context;
        _log = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception, Order Order)> CreateAsync(Order order)
    {
        try
        {
            await _ctx.Orders.AddAsync(order);

            await _ctx.SaveChangesAsync();


            _log.LogInformation($" Order create in DB: {order}");


            return (true, null, order);
        }
      
        catch (Exception e)
        {
            _log.LogInformation($"Create order to DB failed: {e.Message}", e);

            return (false, e, null);
        }

    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Order order)
    {
        try
        {

                _ctx.Orders.Remove(order);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($"Order remove in DB: {order}");


                 return (true, null);
        }

        catch (Exception e)
        {
            _log.LogInformation($"Remove order to DB failed: {e.Message}", e);


            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(int id)
      => _ctx.Orders
    .AnyAsync(p => p.Id == id);

    
    public Task<List<Order>> GetAllAsync()
    => _ctx.Orders

        .AsNoTracking()

        .ToListAsync();
    
    
    public Task<List<Order>> GetIdAsync(int id)
   => _ctx.Orders

       .AsNoTracking()

       .ToListAsync();
  
  
    public Task<Order> GetAsync(int id)
    => _ctx.Orders.FirstOrDefaultAsync(a => a.Id == id);


    public async Task<(bool IsSuccess, Exception Exception, Order Order)> UpdatePostAsync(Order order)
    {
        try
        {
            if (await _ctx.Orders.AnyAsync(t => t.Id == order.Id))
            {
                _ctx.Orders.Update(order);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($" Order update in DB: {order}");


                return (true, null, order);
            }
           
            else
            {

                return (false, new Exception($"Order with given ID: {order.Id} doesnt exist!"), null);
            }
        }
       
        catch (Exception e)
        {
            _log.LogInformation($"Update order to DB failed: {e.Message}", e);
            

            return (false, e, null);
        }
    }

}