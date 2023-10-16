using CleanArchMvc.Application.DTOs; 
using CleanArchMvc.Application.Interfaces;

using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.AspNetCore.Authorization;

namespace CleanArchMvc.WebUI.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly IProductService _service;
    private readonly ICategoryService _categoryService; 
    private readonly IWebHostEnvironment _environment;
    public ProductsController(IProductService service, 
        ICategoryService categoryService, IWebHostEnvironment environment)
    {
        _service = service;
        _categoryService = categoryService;
        _environment = environment; 
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _service.GetAllAsync(); 
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = 
        new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO product)
    {
        if(ModelState.IsValid)
        {
            await _service.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if(id is null)
        {
            return NotFound();
        }
        var product = await _service.GetByIdAsync(id);
        if(product is null)
        {
            return NotFound();
        }
        var categories = await _categoryService.GetAllAsync();
        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO product)
    {
        if(ModelState.IsValid)
        {
            await _service.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if(id is null)
        {
            return NotFound();
        }
        var product = await _service.GetByIdAsync(id);
        if(product is null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        await _service.RemoveAsync(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if(id is null)
        {
            return NotFound();
        }
        var product = await _service.GetByIdAsync(id);
        if(product is null)
        {
            return NotFound();
        }
        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + product.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists; 

        return View(product);
    }


}
