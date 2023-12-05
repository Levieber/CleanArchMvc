using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetByIdAsync(int? id)
    {
        var product = await _context.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id);
        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<Product> RemoveAsync(Product product)
    {
        _context.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
