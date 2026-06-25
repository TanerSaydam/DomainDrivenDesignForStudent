using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Orders;
using DomainDrivenDesign.Domain.Orders.ValueObjects;
using MediatR;
using TS.Result;

namespace DomainDrivenDesign.Application.Orders;

public sealed record OrderUpdateCommand(
    Guid Id,
    DateOnly Date,
    List<OrderItem> Items) : IRequest<Result<string>>;

internal sealed class OrderUpdateCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<OrderUpdateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (order is null)
        {
            return Result<string>.Failure("Order not found");
        }

        OrderDate orderDate = new(request.Date);
        order.SetDate(orderDate);
        order.SetItems(request.Items);
        orderRepository.Update(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Order updated successfully";
    }
}
