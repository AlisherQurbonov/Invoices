using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;

namespace invoice.Controller;


[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{

      private readonly ILogger<InvoiceController> _log;
      private readonly IInvoiceService _ser;
    public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService service)
    {
        _log = logger;
        _ser = service;
    }


    [HttpPost]
    public async Task<IActionResult> CreateInvoiceAsync([FromForm] NewInvoice invoice)
    {
      
            var result = await _ser.CreateAsync(invoice.ToInvoiceEntity());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Invoice create in DB: {invoice.Id}");
           
           
            return Ok( new
            {
                Id = invoice.Id,
                Amount = invoice.Amount,
                Issue = invoice.Issued,
                Due = invoice.Due

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
