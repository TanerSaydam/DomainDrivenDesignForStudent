using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Domain.Products.ValuObjects;
using DomainDrivenDesign.Domain.Shared;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.Application.Products;

public sealed record ProductCreateCommand(
    string Name,
    int Stock,
    decimal price,
    Guid CategoryId) : IRequest<Result<string>>;

internal sealed class ProductCreateCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ProductCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        Name name = new(request.Name);
        Stock stock = new(request.Stock);
        Price price = new(request.price);

        Product product = new(name, stock, price, request.CategoryId);
        productRepository.Add(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Product created successfully.";
    }
}
