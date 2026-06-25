using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Dtos;
using DomainDrivenDesign.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesign.Domain;

public static class Extensions
{
    public static IQueryable<AuditQueryableDto<T>> AddAudit<T>(this IQueryable<Entity> query)
    where T : Entity
    {
        HttpContextAccessor httpContextAccessor = new();
        var userManager = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
        var res = query
            .LeftJoin(userManager.Users, m => m.CreatedUserId, m => m.Id, (entity, createdUser)
                    => new { entity, createdUser })
            .LeftJoin(userManager.Users, m => m.entity.UpdatedUserId, m => m.Id, (e, updatedUser)
                    => new AuditQueryableDto<T> { Entity = (T)e.entity, CreatedUser = e.createdUser!, UpdatedUser = updatedUser })
            .AsQueryable();
        return res;
    }
}
