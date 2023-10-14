using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.DTOs;

using Microsoft.AspNetCore.Mvc;


namespace CleanArchMvc.WebUI.Controllers;

// [Route("[controller]")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service; 
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _service.GetAllAsync(); 
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CategoryDTO category)
    {
        if(ModelState.IsValid)
        {
            await _service.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if(id is null)
        {
            return NotFound();
        }
        var category = await _service.GetByIdAsync(id);
        if(category is null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDTO category)
    {
        if(ModelState.IsValid)
        {
            try 
            {
                await _service.UpdateAsync(category);
            }
            catch(Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if(id is null)
        {
            return NotFound();
        }
        var category = await _service.GetByIdAsync(id);
        if(category is null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
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
        var category = await _service.GetByIdAsync(id);
        if(id is null)
        {
            return NotFound();
        }
        return View(category);
    }
}
