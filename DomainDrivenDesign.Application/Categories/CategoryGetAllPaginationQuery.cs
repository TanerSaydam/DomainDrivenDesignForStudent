using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Dtos;
using Mapster;
using MediatR;

namespace DomainDrivenDesign.Application.Categories;

public sealed record CategoryGetAllPaginationQuery(
    int PageNumber,
    int PageSize,
    string? OrderField,
    string? OrderDir = "asc",
    string? Search = null) : IRequest<PaginationDto<CategoryDto>>;

internal sealed class CategoryGetAllPaginationQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<CategoryGetAllPaginationQuery, PaginationDto<CategoryDto>>
{
    public async Task<PaginationDto<CategoryDto>> Handle(CategoryGetAllPaginationQuery request, CancellationToken cancellationToken)
    {
        var paginationRequest = request.Adapt<PaginationRequestDto>();

        var data = await categoryRepository
            .GetAuditQueryables()
            .Where(p => request.Search == null ? true : p.Entity.Name.Value.ToLower().Contains(request.Search.ToLower()))
            .MapTo()
            .ToPagination(paginationRequest, cancellationToken);

        return data;
    }
}