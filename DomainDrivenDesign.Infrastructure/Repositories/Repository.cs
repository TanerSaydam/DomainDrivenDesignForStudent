using DomainDrivenDesign.Domain;
using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Dtos;
using DomainDrivenDesign.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DomainDrivenDesign.Infrastructure.Repositories;

internal class Repository<T>(
    ApplicationDbContext dbContext) : IRepository<T>
    where T : Entity
{
    public void Add(T entity)
    {
        dbContext.Add(entity);
    }

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        dbContext.Update(entity);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public IQueryable<T> GetAll()
    {
        return dbContext.Set<T>().AsQueryable();
    }

    public IQueryable<AuditQueryableDto<T>> GetAuditQueryables()
    {
        return dbContext.Set<T>().AsQueryable().AddAudit<T>();
    }

    public void Update(T entity)
    {
        dbContext.Update(entity);
    }
}
