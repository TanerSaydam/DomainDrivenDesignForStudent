using DomainDrivenDesign.Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DomainDrivenDesign.Application;

public static class Extensions
{
    public static async Task<PaginationDto<T>> ToPagination<T>(
       this IQueryable<T> query,
       PaginationRequestDto request,
       CancellationToken cancellationToken = default)
       where T : class
    {
        var totalCount = await query.CountAsync(cancellationToken);
        var data = query.AsQueryable();

        if (!string.IsNullOrEmpty(request.OrderField))
        {
            var parameter = Expression.Parameter(typeof(T), "x"); //x =>
            var property = Expression.PropertyOrField(parameter, request.OrderField); //x.Name
            var lambda = Expression.Lambda(property, parameter); //x => x.Name

            string methodName = request.OrderDir?.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

            var expression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.Type },
                data.Expression,
                Expression.Quote(lambda)
            );

            data = data.Provider.CreateQuery<T>(expression);
        }

        var res = await data
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginationDto<T>
        {
            Data = res,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}