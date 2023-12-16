using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService(IMapper mapper, IMediator mediator) : IProductService
{
    public async Task<ProductDTO> Add(ProductDTO productDto)
    {
        var productCreateCommand = mapper.Map<ProductCreateCommand>(productDto);
        return mapper.Map<ProductDTO>(await mediator.Send(productCreateCommand));
    }

    public async Task<ProductDTO> GetByIdAsync(int id)
    {
        var productByIdQuery = new GetProductByIdQuery(id) ?? throw new ApplicationException("Product could not be loaded");

        var result = await mediator.Send(productByIdQuery);

        return mapper.Map<ProductDTO>(result);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var productsQuery = new GetProductsQuery() ?? throw new ApplicationException("Product could not be loaded");

        var result = await mediator.Send(productsQuery);

        return mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task Remove(int id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id) ?? throw new ApplicationException("Product could not be loaded");
        await mediator.Send(productRemoveCommand);
    }

    public async Task Update(ProductDTO productDto)
    {
        var productUpdateCommand = mapper.Map<ProductUpdateCommand>(productDto);
        await mediator.Send(productUpdateCommand);
    }
}
