using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Orders;
using DomainDrivenDesign.Domain.Orders.ValueObjects;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.Application.Orders;

public sealed record OrderCreateCommand(
    DateOnly Date,
    List<OrderItem> Items) : IRequest<Result<string>>;

internal sealed class OrderCreateCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<OrderCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
        OrderDate orderDate = new(request.Date);
        Order order = Order.Create(orderDate, request.Items);

        orderRepository.Add(order);
        await unitOfWork.SaveChangesAsync();

        return "Order created successfully";
    }
}