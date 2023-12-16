using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Index()
    {
        var products = await productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Show(int id)
    {
        var product = await productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound("Product not found!");
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ProductDTO productDto)
    {
        if (productDto is null)
        {
            return BadRequest("Invalid data");
        }

        var product = await productService.Add(productDto);

        return CreatedAtAction(nameof(Show), new { id = product.Id }, product);
    }

    [HttpPut]
    public async Task<ActionResult> Update(ProductDTO productDto)
    {
        var product = await productService.GetByIdAsync(productDto.Id);

        if (product is null)
        {
            return NotFound("Product not found!");
        }

        await productService.Update(product);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound("Product not found!");
        }

        await productService.Remove(product.Id);

        return NoContent();
    }
}

