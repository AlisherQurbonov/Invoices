using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;

namespace invoice.Controller;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

      private readonly ILogger<ProductController> _log;
      private readonly IProductService _ser;
    public ProductController(ILogger<ProductController> logger, IProductService service)
    {
        _log = logger;
        _ser = service;
    }


    [HttpPost]
    public async Task<IActionResult> CreateInvoiceAsync( NewProduct product)
    {
      
            var result = await _ser.CreateAsync(product.ToProductEntity());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Invoice create in DB: {product.Id}");
           
           
            return Ok( new
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Photo = product.Photo

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
