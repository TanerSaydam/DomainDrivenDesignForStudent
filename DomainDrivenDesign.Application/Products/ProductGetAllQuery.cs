using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Products;
using MediatR;

namespace DomainDrivenDesign.Application.Products;

public sealed record ProductGetAllQuery() : IRequest<IQueryable<ProductDto>>;

internal sealed class ProductGetAllQueryHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository) : IRequestHandler<ProductGetAllQuery, IQueryable<ProductDto>>
{
    public Task<IQueryable<ProductDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
    {
        var res = productRepository.GetAuditQueryables().MapTo(categoryRepository.GetAll());

        return Task.FromResult(res);
    }
}