using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;

public class ProductRemoveCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductRemoveCommand, Product>
{
    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id) ?? throw new ApplicationException("Product could not be found");

        return await productRepository.RemoveAsync(product);
    }
}
