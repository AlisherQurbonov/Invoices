using Microsoft.AspNetCore.Mvc;
using invoice.Services;
using invoice.Models;
using invoice.Mapper;
using invoice.Entities;

namespace invoice.Controller;


[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{

      private readonly ILogger<CategoryController> _log;
    
      private readonly ICategoryService _ser;

    private readonly IProductService _pc;
    public CategoryController(ILogger<CategoryController> logger, ICategoryService service , IProductService service1)
    {
        _log = logger;
        _ser = service;
        _pc = service1;
    }


    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromForm] NewCategory category)
    {


            var result = await _ser.CreateAsync(category.ToCategoryEntities());

           try
           {

            if(result.IsSuccess)
            
            {
           
            _log.LogInformation($"Category create in DB: {category.Id}");
           
           
            return Ok( new
            {
                Id = category.Id,
                Name = category.Name,

            });

            }

           }

           catch(Exception e)
           
           {
                _log.LogInformation($"Create category to DB failed: {e.Message}", e);
           }

          
          return BadRequest(result.Exception.Message);

    }



        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {

            var category = await _ser.GetAllAsync();

            return Ok(category
                .Select(i =>
                {
                    return new {
                        Name = i.Name,
                        Product = i.Products
                    };
              }));
        }



        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetCategory([FromRoute]int Id)
        {
            var category = await _ser.GetAsync(Id);
               

            if(category is default(Category))
            {
                return NotFound($"Category with ID {Id} not found");
            }

            return Ok(category);
        }








}
