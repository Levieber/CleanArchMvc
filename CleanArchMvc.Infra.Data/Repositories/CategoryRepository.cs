using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        context.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> GetByIdAsync(int? id)
    {
        var category = await context.Categories.FindAsync(id);
        return category;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var categories = await context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category> RemoveAsync(Category category)
    {
        context.Remove(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        context.Update(category);
        await context.SaveChangesAsync();
        return category;
    }
}
