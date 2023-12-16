using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Index()
    {
        var categories = await categoryService.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> Show(int id)
    {
        var category = await categoryService.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound("Category not found!");
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CategoryDTO categoryDto)
    {
        if (categoryDto is null)
        {
            return BadRequest("Invalid data");
        }

        var category = await categoryService.Add(categoryDto);

        return CreatedAtAction(nameof(Show), new { id = category.Id }, category);
    }

    [HttpPut]
    public async Task<ActionResult> Update(CategoryDTO categoryDto)
    {
        var category = await categoryService.GetByIdAsync(categoryDto.Id);

        if (category is null)
        {
            return NotFound("Category not found!");
        }

        await categoryService.Update(category);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var category = await categoryService.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound("Category not found!");
        }

        await categoryService.Remove(category.Id);

        return NoContent();
    }
}
