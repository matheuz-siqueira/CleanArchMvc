using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;


[Produces("application/json")]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoryController(ICategoryService service)
    {
        _service = service; 
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
    {
        var cateogries = await _service.GetAllAsync();
        if(cateogries is null)
        {
            return NotFound(new { message = "Categories not found."});
        }
        return Ok(cateogries);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id); 
        if(category is null)
        {
            return NotFound(new { message = "Category not found."});
        }
        return Ok(category); 
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CategoryDTO category)
    {
        if(category is null)
        {
            return BadRequest(new { message = "invalid data."});
        }
        await _service.AddAsync(category); 
        return new CreatedAtRouteResult("GetCategory", new { id = category.Id}, category);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CategoryDTO category)
    {
        if(id != category.Id)
        {
            return BadRequest();
        }
        if(category is null)
        {
            return BadRequest(); 
        }
        await _service.UpdateAsync(category);
        return Ok(category); 
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> Delete([FromRoute] int id)
    {
        var category = await _service.GetByIdAsync(id);
        if(category is null)
        {
            return NotFound(new { message = "Category not found." });
        }
        await _service.RemoveAsync(id);
        return Ok(category); 
    }
}
