using DomainDrivenDesign.Domain.Abstractions;

namespace DomainDrivenDesign.Domain.Orders;

public interface IOrderRepository : IRepository<Order>
{
}