using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;
using invoice.Data;


namespace invoice.Controller;


[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{

      private readonly ILogger<InvoiceController> _log;
      private readonly IInvoiceService _ser;

     private readonly InvoiceDbContext _context;
    public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService service, InvoiceDbContext context)
    {
        _log = logger;
        _ser = service;
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> CreateInvoiceAsync([FromForm] NewInvoice invoice)
    {
          
          var order = _context.Invoices.OrderBy(p => p.Issued).LastOrDefault();


         var invoice1 = invoice.ToInvoiceEntity();

       
        if (order == null || order.Issued < DateTimeOffset.UtcNow.ToLocalTime())
        {
            invoice1.Issued = DateTimeOffset.UtcNow.ToLocalTime();
            invoice1.Due = invoice1.Issued.AddDays(15);

        }

        else
        {
            invoice1.Issued = order.Issued.AddDays(15);
            invoice1.Due = invoice1.Issued;
        }


      
            var result = await _ser.CreateAsync(invoice1);

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Invoice create in DB: {invoice.Id}");
           
           
            return Ok( new
            {
                Id = invoice.Id,
                Ord_Id = invoice.Ord_Id,
                Amount = invoice.Amount,
                Issued = invoice.Issued,
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


    [HttpGet]

    public async Task<IActionResult> GetDue()
    {

        var order = _context.Invoices.OrderBy(p => p.Issued).LastOrDefault();


       
        if (order == null || order.Issued < DateTimeOffset.UtcNow.ToLocalTime())
        {
            order.Issued = DateTimeOffset.UtcNow.ToLocalTime();
            order.Due = order.Issued.AddDays(15);

        }

        else
        {
            order.Issued = order.Issued.AddDays(15);
            order.Due = order.Issued;
            var invoice = await _ser.GetAllAsync();
            return Ok(invoice.Select(i =>
                {
                    return new {
                        Id = i.Id,
                        Amount = i.Amount,
                        Due = i.Due,
                        Ord_Id = i.Ord_Id

                    };
              }));
        }
          
           return NotFound("No invoice exist!");

      
      }




        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetIdInvoice(int Id)
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
                     Amount = i.Amount,
                     Due = i.Due,
                     Ord_Id = i.Ord_Id,
                     Issued = i.Issued,
                     Order = i.Order,
                     Payments = i.Payments.ToList()
                };
              }));
        }

    



}
