using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
{
    public async Task<CategoryDTO> Add(CategoryDTO categoryDto)
    {
        var categoryEntity = mapper.Map<Category>(categoryDto);
        return mapper.Map<CategoryDTO>(await categoryRepository.CreateAsync(categoryEntity));
    }

    public async Task<CategoryDTO> GetByIdAsync(int id)
    {
        var categoryEntity = await categoryRepository.GetByIdAsync(id);
        return mapper.Map<CategoryDTO>(categoryEntity);
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
    {
        var categoriesEntity = await categoryRepository.GetCategoriesAsync();
        return mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
    }

    public async Task Remove(int id)
    {
        var categoryEntity = categoryRepository.GetByIdAsync(id).Result;

        if (categoryEntity is not null)
        {
            await categoryRepository.RemoveAsync(categoryEntity);
        }
    }

    public async Task Update(CategoryDTO categoryDto)
    {
        var categoryEntity = mapper.Map<Category>(categoryDto);
        await categoryRepository.UpdateAsync(categoryEntity);
    }
}
