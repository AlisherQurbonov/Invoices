using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;


namespace invoice.Controller;


[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{

      private readonly ILogger<OrderController> _log;
      private readonly IOrderService _ser;
    public OrderController(ILogger<OrderController> logger, IOrderService service)
    {
        _log = logger;
        _ser = service;
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync( NewOrder order)
    {
      
            var result = await _ser.CreateAsync(order.ToOrderEntity());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Invoice create in DB: {order.Id}");
           
           
            return Ok( new
            {
                Id = order.Id,
                Date = order.Date,

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