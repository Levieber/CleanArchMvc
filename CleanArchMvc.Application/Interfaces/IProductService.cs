using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
    Task<ProductDTO> GetByIdAsync(int? id);
    Task<ProductDTO> Add(ProductDTO productDto);
    Task Update(ProductDTO productDto);
    Task Remove(int? id);
}
