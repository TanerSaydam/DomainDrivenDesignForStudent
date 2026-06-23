using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DomainDrivenDesign.Application.Categories;

public sealed record CategoryGetAllODataQuery() : IRequest<IQueryable<CategoryDto>>;

internal sealed class CategoryGetAllODataQueryHandler(
    ICategoryRepository categoryRepository,
    UserManager<User> userManager) : IRequestHandler<CategoryGetAllODataQuery, IQueryable<CategoryDto>>
{
    public Task<IQueryable<CategoryDto>> Handle(CategoryGetAllODataQuery request, CancellationToken cancellationToken)
    {
        var res = categoryRepository.GetAll()
            .LeftJoin(userManager.Users, m => m.CreatedUserId, m => m.Id, (category, user) => new { category, user })
            .AddAudit()
            .Select(s => new CategoryDto()
            {
                Id = s.category.Id,
                Name = s.category.Name.Value,
                CreatedDate = s.category.CreatedDate,
                CreatedUserId = s.category.CreatedUserId,
                CreatedUserName = s.user!.FullName,
                UpdatedDate = s.category.UpdatedDate,
                UpdatedUserId = s.category.UpdatedUserId,
                UpdatedUserName = s.category.UpdatedUserId == null ? null : s.user.FullName
            });

        return Task.FromResult(res);
    }
}