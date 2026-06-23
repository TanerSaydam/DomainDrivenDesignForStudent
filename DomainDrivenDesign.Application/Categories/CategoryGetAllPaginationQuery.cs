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

        var data = await categoryRepository.GetAll()
            .Where(p => request.Search == null ? true : p.Name.Value.ToLower().Contains(request.Search.ToLower()))
            .AddAudit()
            .Select(s => new CategoryDto()
            {
                Id = s.Id,
                Name = s.Name.Value,
                CreatedDate = s.CreatedDate,
                CreatedUserId = s.CreatedUserId,
                CreatedUserName = "",
                UpdatedDate = s.UpdatedDate,
                UpdatedUserId = s.UpdatedUserId,
                UpdatedUserName = s.UpdatedUserId == null ? null : ""
            })
            .ToPagination(paginationRequest, cancellationToken);

        return data;
    }
}