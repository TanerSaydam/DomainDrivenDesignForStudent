using DomainDrivenDesign.Domain.Categories;
using MediatR;

namespace DomainDrivenDesign.Application.Categories;

public sealed record CategoryGetAllODataQuery() : IRequest<IQueryable<CategoryDto>>;

internal sealed class CategoryGetAllODataQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<CategoryGetAllODataQuery, IQueryable<CategoryDto>>
{
    public Task<IQueryable<CategoryDto>> Handle(CategoryGetAllODataQuery request, CancellationToken cancellationToken)
    {
        var res = categoryRepository
            .GetAuditQueryables()
            .MapTo();

        return Task.FromResult(res);
    }
}