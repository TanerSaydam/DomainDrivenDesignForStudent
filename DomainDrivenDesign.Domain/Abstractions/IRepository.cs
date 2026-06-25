using DomainDrivenDesign.Domain.Dtos;
using System.Linq.Expressions;

namespace DomainDrivenDesign.Domain.Abstractions;

public interface IRepository<T>
    where T : Entity
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> GetAll();
    IQueryable<AuditQueryableDto<T>> GetAuditQueryables();
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}