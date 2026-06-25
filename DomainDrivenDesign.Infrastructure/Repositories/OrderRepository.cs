using DomainDrivenDesign.Domain.Orders;
using DomainDrivenDesign.Infrastructure.Context;

namespace DomainDrivenDesign.Infrastructure.Repositories;

internal sealed class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}