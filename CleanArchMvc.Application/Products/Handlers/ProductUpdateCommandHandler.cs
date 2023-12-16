using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;

public class ProductUpdateCommandHandler(IProductRepository productRepository) : IRequestHandler<ProductUpdateCommand, Product>
{
    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id) ?? throw new ApplicationException("Product could not be found");

        product.Update(request.Name, request.Description, request.Price, request.Stock, request.Image, request.CategoryId);

        return await productRepository.UpdateAsync(product);
    }
}
