using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Dtos;

namespace DomainDrivenDesign.Application.Categories;

public sealed class CategoryDto : AuditDto
{
    public string Name { get; set; } = default!;
}

public static class CategoryExtensions
{
    public static IQueryable<CategoryDto> MapTo(this IQueryable<AuditQueryableDto<Category>> query)
    {
        var res = query.AsQueryable()
            .Select(s => new CategoryDto()
            {
                Id = s.Entity.Id,
                Name = s.Entity.Name.Value,
                CreatedDate = s.Entity.CreatedDate,
                CreatedUserId = s.Entity.CreatedUserId,
                CreatedUserName = s.CreatedUser!.FullName,
                UpdatedDate = s.Entity.UpdatedDate,
                UpdatedUserId = s.Entity.UpdatedUserId,
                UpdatedUserName = s.Entity.UpdatedUserId == null ? null : s.UpdatedUser!.FullName
            });
        return res;
    }

    public static IQueryable<CategoryDto> MapTo(this IQueryable<Category> query)
    {
        var res = query.AsQueryable()
            .Select(s => new CategoryDto()
            {
                Id = s.Id,
                Name = s.Name.Value
            });
        return res;
    }
}
