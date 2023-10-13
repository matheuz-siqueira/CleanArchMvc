using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper; 
    }
    public async Task AddAsync(ProductDTO productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _repository.CreateAsync(product); 
    }

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(products); 
    }

    public async Task<ProductDTO> GetByCategoryAsync(int? id)
    {
        var product = await _repository.GetByCategoryAsync(id);
        return _mapper.Map<ProductDTO>(product); 
    }

    public async Task<ProductDTO> GetByIdAsync(int? id)
    {
        var product = await _repository.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task RemoveAsync(int? id)
    {
        var product = _repository.GetByIdAsync(id).Result;
        await _repository.RemoveAsync(product);
    }

    public async Task UpdateAsync(ProductDTO productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _repository.UpdateAsync(product);
    }
}
