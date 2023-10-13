using CleanArchMvc.Application.Interfaces;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchMvc.WebUI.Controllers;

// [Route("[controller]")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service; 
    }

    [HttpGet()]
    public async Task<IActionResult> Index()
    {
        var categories = await _service.GetAllAsync(); 
        return View(categories);
    }

    
}
