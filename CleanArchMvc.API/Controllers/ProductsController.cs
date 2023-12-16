using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Index()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Show(int id)
    {
        var product = await _productService.GetByIdAsync(id);

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

        var product = await _productService.Add(productDto);

        return CreatedAtAction(nameof(Show), new { id = product.Id }, product);
    }

    [HttpPut]
    public async Task<ActionResult> Update(ProductDTO productDto)
    {
        var product = await _productService.GetByIdAsync(productDto.Id);

        if (product is null)
        {
            return NotFound("Product not found!");
        }

        await _productService.Update(product);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound("Product not found!");
        }

        await _productService.Remove(product.Id);

        return NoContent();
    }
}

