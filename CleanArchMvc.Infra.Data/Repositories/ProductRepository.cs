using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _contex;
    public ProductRepository(ApplicationDbContext contex)
    {
        _contex = contex; 
    }

    public async Task<Product> CreateAsync(Product product)
    {
        await _contex.Products.AddAsync(product); 
        await _contex.SaveChangesAsync(); 
        return product;        
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _contex.Products.AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        return await _contex.Products.Include(c => c.Category)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> RemoveAsync(Product product)
    {
        _contex.Products.Remove(product);
        await _contex.SaveChangesAsync(); 
        return product; 
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _contex.Products.Update(product); 
        await _contex.SaveChangesAsync(); 
        return product;
    }
}
