using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;

namespace invoice.Controller;


[ApiController]
[Route("[controller]")]
public class DetailController : ControllerBase
{

      private readonly ILogger<DetailController> _log;
      private readonly IDetailService _ser;
    public DetailController(ILogger<DetailController> logger, IDetailService service)
    {
        _log = logger;
        _ser = service;
    }


    [HttpPost]
    public async Task<IActionResult> CreateInvoiceAsync( NewDetail detail)
    {
      
            var result = await _ser.CreateAsync(detail.ToDetailEntity());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Invoice create in DB: {detail.Id}");
           
           
            return Ok( new
            {
                Id = detail.Id,
                Quantity = detail.Quantity,

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
