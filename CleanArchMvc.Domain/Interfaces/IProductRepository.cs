using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface IProductRepository
{  
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(); 
    Task<IEnumerable<Product>> GetByCategoryAsync(int? id);
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<Product> RemoveAsync(Product product); 
}