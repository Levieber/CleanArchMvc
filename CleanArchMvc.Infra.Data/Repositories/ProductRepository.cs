using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product)
    {
        context.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetByIdAsync(int? id)
    {
        var product = await context.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id);
        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = await context.Products.ToListAsync();
        return products;
    }

    public async Task<Product> RemoveAsync(Product product)
    {
        context.Remove(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        context.Update(product);
        await context.SaveChangesAsync();
        return product;
    }
}
