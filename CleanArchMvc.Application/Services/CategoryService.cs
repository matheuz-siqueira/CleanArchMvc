using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository; 
        _mapper = mapper;
    }

    public async Task AddAsync(CategoryDTO categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _repository.CreateAsync(category);
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }

    public async Task<CategoryDTO> GetByIdAsync(int? id)
    {
        var category = await _repository.GetByIdAsync(id); 
        return _mapper.Map<CategoryDTO>(category); 
    }

    public async Task RemoveAsync(int? id)
    {
        var category = _repository.GetByIdAsync(id).Result;
        await _repository.RemoveAsync(category);

    }

    public async Task UpdateAsync(CategoryDTO categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _repository.UpdateAsync(category);
    }
}
