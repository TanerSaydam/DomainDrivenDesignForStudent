using DomainDrivenDesign.Domain.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace DomainDrivenDesign.Application.Categories;

public sealed record CategoryGetQuery(
    Guid Id) : IRequest<Result<CategoryDto>>;

internal sealed class CategoryGetQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<CategoryGetQuery, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(CategoryGetQuery request, CancellationToken cancellationToken)
    {
        var res =
            await categoryRepository
            .GetAuditQueryables()
            .Where(i => i.Entity.Id == request.Id)
            .MapTo()
            .FirstOrDefaultAsync();

        if (res is null)
        {
            return Result<CategoryDto>.Failure("Category not found");
        }

        return res;
    }
}