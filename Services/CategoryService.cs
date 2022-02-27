using invoice.Data;
using invoice.Entities;
using Microsoft.EntityFrameworkCore;

namespace invoice.Services;

public class CategoryService : ICategoryService
{

    private readonly InvoiceDbContext _ctx;

    private readonly ILogger<CategoryService> _log;


    public CategoryService(ILogger<CategoryService> logger, InvoiceDbContext context)
    {
        _ctx = context;


        _log = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception, Category Category)> CreateAsync(Category category)
    {
        try
        {
            await _ctx.Categories.AddAsync(category);
            
            
            await _ctx.SaveChangesAsync();

           
            _log.LogInformation($" Category create in DB: {category}");

           
            return (true, null, category);
        }
       
        catch (Exception e)
        {
            _log.LogInformation($"Create category to DB failed: {e.Message}", e);
           
           
            return (false, e, null);
        }

    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Category category)
    {
        try
        {
 
            _ctx.Categories.Remove(category);

           
            
             await _ctx.SaveChangesAsync();

         
          
            _log.LogInformation($"Category remove in DB: {category}");
            
           
          
            return (true, null);
        }

       
        catch (Exception e)
        {
            _log.LogInformation($"Remove category to DB failed: {e.Message}", e);

           
            return (false, e);
        }
    }

    public Task<bool> ExistsAsync(int id)
      => _ctx.Categories
    .AnyAsync(p => p.Id == id);

 
    public Task<List<Category>> GetAllAsync()
    => _ctx.Categories
             
        .AsNoTracking()

        .ToListAsync();
   
    public Task<List<Category>> GetIdAsync(int id)
   => _ctx.Categories

       .AsNoTracking()

       .Where(i => i.Id == id)

       .ToListAsync();
   
   
    public Task<Category> GetAsync(int id)
    => _ctx.Categories.FirstOrDefaultAsync(a => a.Id == id);


    public async Task<(bool IsSuccess, Exception Exception, Category Category)> UpdatePostAsync(Category category)
    {
        try
        {
            if (await _ctx.Categories.AnyAsync(t => t.Id == category.Id))
            {
              
                _ctx.Categories.Update(category);


                await _ctx.SaveChangesAsync();


                _log.LogInformation($" Category update in DB: {category}");



                return (true, null, category);
            }


            else
            {

                return (false, new Exception($"Category with given ID: {category.Id} doesnt exist!"), null);
            }
        }


        catch (Exception e)
        {

            _log.LogInformation($"Update category to DB failed: {e.Message}", e);



            return (false, e, null);
        }
    }

}