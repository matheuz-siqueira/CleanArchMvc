using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllAsync();
    Task<ProductDTO> GetByCategoryAsync(int? id);
    Task<ProductDTO> GetByIdAsync(int? id); 
    Task AddAsync(ProductDTO productDto); 
    Task UpdateAsync(ProductDTO productDto);
    Task RemoveAsync(int? id);  
}
