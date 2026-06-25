using DomainDrivenDesign.Domain.Orders;
using DomainDrivenDesign.Domain.Products;
using MediatR;

namespace DomainDrivenDesign.Application.Orders;

public sealed record OrderGetAllQuery() : IRequest<IQueryable<OrderDto>>;

internal sealed class OrderGetAllQueryHandler(
    IOrderRepository orderRepository,
    IProductRepository productRepository) : IRequestHandler<OrderGetAllQuery, IQueryable<OrderDto>>
{
    public Task<IQueryable<OrderDto>> Handle(OrderGetAllQuery request, CancellationToken cancellationToken)
    {
        var res = orderRepository.GetAuditQueryables().MapTo(productRepository.GetAll());

        return Task.FromResult(res);
    }
}