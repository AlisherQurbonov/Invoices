using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;
using invoice.Data;

namespace invoice.Controller;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{

      private readonly ILogger<CustomerController> _log;
      private readonly ICustomerService _ser;

    private readonly InvoiceDbContext _con;
    public CustomerController(ILogger<CustomerController> logger, ICustomerService service, InvoiceDbContext context)
    {
        _log = logger;
        _ser = service;
        _con = context;
    }


    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync([FromForm] NewCustomer customer)
    {
      
            var result = await _ser.CreateAsync(customer.ToCustomerEntity());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Invoice create in DB: {customer.Id}");
           
           
            return Ok( new
            {
                Id = customer.Id,
                Name = customer.Name,
                Country = customer.Country,
                Text = customer.Text,
                Phone = customer.Phone

            });

            }

           }

           catch(Exception e)
           
           {
                _log.LogInformation($"Create invoice to DB failed: {e.Message}", e);
           }

          
          return BadRequest(result.Exception.Message);

    }



     [HttpGet]

    public async Task<IActionResult> GetCustomer()
    {
       
         var order = _con.Orders.OrderBy(o=>o.Id).OrderBy(p => p.Date).LastOrDefault();

       if(order==null || order.Date < DateTimeOffset.UtcNow.ToLocalTime() )
       {

         var customer = await _ser.GetAllAsync();

            return Ok(customer.Select(i =>
                {
                    return new {
                        Id = i.Id,
                        Name = i.Name,
                        Country = i.Country,
                        Text = i.Text,
                        Phone = i.Phone

                    };
              }));
       }

        
          
           return NotFound("No invoice exist!");

      
      }
}