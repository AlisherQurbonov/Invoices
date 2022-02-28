using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;

namespace invoice.Controller;


[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{

      private readonly ILogger<PaymentController> _log;
      private readonly IPaymentService _ser;
    public PaymentController(ILogger<PaymentController> logger, IPaymentService service)
    {
        _log = logger;
        _ser = service;
    }


    [HttpPost]
    public async Task<IActionResult> CreatePaymentAsync([FromForm] NewPayment payment)
    {
      
            var result = await _ser.CreateAsync(payment.ToPaymentEntity());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Payment create in DB: {payment.Id}");
           
           
            return Ok( new
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Time = payment.Time,

            });

            }

           }

           catch(Exception e)
           
           {
                _log.LogInformation($"Payment invoice to DB failed: {e.Message}", e);
           }

          
          return BadRequest(result.Exception.Message);

    }


        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetIdPayment(int Id)
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
                    Amount = i.Amount,
                    Time = i.Time,
                    Inv_Id = i.Inv_Id
                };
              }));
        }






}

