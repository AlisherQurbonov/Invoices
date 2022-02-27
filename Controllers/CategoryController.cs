using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;

namespace invoice.Controller;


[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{

      private readonly ILogger<CategoryController> _log;
      private readonly ICategoryService _ser;
    public CategoryController(ILogger<CategoryController> logger, ICategoryService service)
    {
        _log = logger;
        _ser = service;
    }


    [HttpPost]
    public async Task<IActionResult> CreateInvoiceAsync( NewCategory category)
    {
      
            var result = await _ser.CreateAsync(category.ToCategoryEntities());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Invoice create in DB: {category.Id}");
           
           
            return Ok( new
            {
                Id = category.Id,
                Name = category.Name,

            });

            }

           }

           catch(Exception e)
           
           {
                _log.LogInformation($"Create invoice to DB failed: {e.Message}", e);
           }

          
          return BadRequest(result.Exception.Message);

    }






}
