using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace DomainDrivenDesign.Application.Categories;

public sealed record CategoryGetQuery(
    Guid Id) : IRequest<Result<CategoryDto>>;

public sealed class CategoryDto : AuditDto
{
    public string Name { get; set; } = default!;
}

internal sealed class CategoryGetQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<CategoryGetQuery, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(CategoryGetQuery request, CancellationToken cancellationToken)
    {
        var res =
            await categoryRepository
            .GetAll()
            .Where(i => i.Id == request.Id)
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
            .FirstOrDefaultAsync();

        if (res is null)
        {
            return Result<CategoryDto>.Failure("Category not found");
        }

        return res;
    }
}