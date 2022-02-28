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
    public async Task<IActionResult> CreateOrderAsync([FromForm] NewOrder order)
    {
      
            var result = await _ser.CreateAsync(order.ToOrderEntity());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Order create in DB: {order.Id}");
           
           
            return Ok( new
            {
                Id = order.Id,
                Date = order.Date,

            });

            }

           }

           catch(Exception e)
           
           {
                _log.LogInformation($"Order invoice to DB failed: {e.Message}", e);
           }

          
          return BadRequest(result.Exception.Message);

    }



        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetIdOrder(int Id)
        {
      
           if(!await _ser.ExistsAsync(Id))
        {
            return NotFound();
        }

            var images = await _ser.GetIdAsync(Id);

            return Ok(images
                .Select(i =>
                {
                    return new {
                    Id = i.Id,
                    Invoice = i.Invoices,
                    Date = i.Date,
                    Cust_Id = i.Cust_Id
                };
              }));
        }




}