using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Users;

namespace DomainDrivenDesign.Application;

public sealed class AuditQueryable<T>
    where T : Entity
{
    public T Entity { get; set; } = default!;
    public User CreatedUser { get; set; } = default!;
    public User? UpdatedUser { get; set; }
}