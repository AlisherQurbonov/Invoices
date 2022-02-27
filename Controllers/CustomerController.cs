using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;


namespace invoice.Controller;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{

      private readonly ILogger<CustomerController> _log;
      private readonly ICustomerService _ser;
    public CustomerController(ILogger<CustomerController> logger, ICustomerService service)
    {
        _log = logger;
        _ser = service;
    }


    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync( NewCustomer customer)
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
}