using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private IMapper _mapper;
    private IMediator _mediator;

    public ProductService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ProductDTO> Add(ProductDTO productDto)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto);
        return _mapper.Map<ProductDTO>(await _mediator.Send(productCreateCommand));
    }

    public async Task<ProductDTO> GetByIdAsync(int? id)
    {
        var productByIdQuery = new GetProductByIdQuery(id.Value) ?? throw new ApplicationException("Product could not be loaded");

        var result = await _mediator.Send(productByIdQuery);

        return _mapper.Map<ProductDTO>(result);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var productsQuery = new GetProductsQuery() ?? throw new ApplicationException("Product could not be loaded");

        var result = await _mediator.Send(productsQuery);

        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task Remove(int? id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id.Value) ?? throw new ApplicationException("Product could not be loaded");
        await _mediator.Send(productRemoveCommand);
    }

    public async Task Update(ProductDTO productDto)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);
        await _mediator.Send(productUpdateCommand);
    }
}
