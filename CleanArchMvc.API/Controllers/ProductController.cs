using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.DTOs;

using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Authorization;

namespace CleanArchMvc.API.Controllers;

[Route("ap/products")]
[Produces("application/json")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    public ProductController(IProductService service)
    {
        _service = service; 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
    {
        var products = await _service.GetAllAsync();
        if(products is null)
        {
            return NotFound(new { message = "Products not found."});
        }
        return Ok(products);
    }
    
    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> GetById([FromRoute] int id)
    {
        var product = await _service.GetByIdAsync(id);
        if(product is null)
        {
            return NotFound(new { message = "Product not found."});
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProductDTO product)
    {
        if(product is null)
        {
            return BadRequest(new { message = "Invalid data. "});
        }
        await _service.AddAsync(product);
        return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Update(
            [FromRoute] int id, [FromBody] ProductDTO product)
    {
        if(id != product.Id)
        {
            return BadRequest();
        }
        if(product is null)
        {
            return BadRequest(); 
        }
        await _service.UpdateAsync(product); 
        return Ok(product); 
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Delete([FromRoute] int id)
    {
        var product = await _service.GetByIdAsync(id);
        if(product is null)
        {
            return NotFound(new { message = "Product not found."});
        }
        await _service.RemoveAsync(id);
        return Ok(product);
    }
}
