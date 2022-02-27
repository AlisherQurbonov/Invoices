using invoice.Entities;
using invoice.Data;
using Microsoft.EntityFrameworkCore;

namespace invoice.Services;

public class ProductService : IProductService
{

    private readonly InvoiceDbContext _ctx;
    private readonly ILogger<ProductService> _log;


    public ProductService(ILogger<ProductService> logger, InvoiceDbContext context)
    {
        _ctx = context;
        _log = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception, Product Product)> CreateAsync(Product product)
    {
        try
        {
            await _ctx.Products.AddAsync(product);

            await _ctx.SaveChangesAsync();


            _log.LogInformation($" Product create in DB: {product}");


            return (true, null, product);
        }
      
        catch (Exception e)
        {
            _log.LogInformation($"Create product to DB failed: {e.Message}", e);

            return (false, e, null);
        }

    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Product product)
    {
        try
        {

                _ctx.Products.Remove(product);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($"Product remove in DB: {product}");


                 return (true, null);
        }

        catch (Exception e)
        {
            _log.LogInformation($"Remove product to DB failed: {e.Message}", e);


            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(int id)
      => _ctx.Products
    .AnyAsync(p => p.Id == id);

    
    public Task<List<Product>> GetAllAsync()
    => _ctx.Products

        .AsNoTracking()

        .ToListAsync();
    
    
    public Task<List<Product>> GetIdAsync(int id)
   => _ctx.Products

       .AsNoTracking()

       .ToListAsync();
  
  
    public Task<Product> GetAsync(int id)
    => _ctx.Products.FirstOrDefaultAsync(a => a.Id == id);


    public async Task<(bool IsSuccess, Exception Exception, Product Product)> UpdatePostAsync(Product product)
    {
        try
        {
            if (await _ctx.Products.AnyAsync(t => t.Id == product.Id))
            {
                _ctx.Products.Update(product);

                await _ctx.SaveChangesAsync();

                _log.LogInformation($" Product update in DB: {product}");
                

                return (true, null, product);
            }
           
            else
            {

                return (false, new Exception($"Product with given ID: {product.Id} doesnt exist!"), null);
            }
        }
        catch (Exception e)
        {
            _log.LogInformation($"Update product to DB failed: {e.Message}", e);

            return (false, e, null);
        }
    }

}