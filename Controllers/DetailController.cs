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
    public async Task<IActionResult> CreateDetailAsync([FromForm] NewDetail detail)
    {
      
            var result = await _ser.CreateAsync(detail.ToDetailEntity());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Detail create in DB: {detail.Id}");
           
           
            return Ok( new
            {
                Id = detail.Id,
                Quantity = detail.Quantity,
                Ord_Id = detail.Ord_Id,
                Pr_Id = detail.Pr_Id
            });

            }

           }

           catch(Exception e)
           
           {
                _log.LogInformation($"Create detail to DB failed: {e.Message}", e);
           }

          
          return BadRequest(result.Exception.Message);

    }


       [HttpGet]
        public async Task<IActionResult> GetDetail()
        {

            var detail = await _ser.GetAllAsync();

            return Ok(detail
                .Select(i =>
                {
                    return new {
                    Quantity = i.Quantity,
                    Ord_Id = i.Ord_Id,
                    Pr_Id = i.Pr_Id
                };
              }));
        }






}
