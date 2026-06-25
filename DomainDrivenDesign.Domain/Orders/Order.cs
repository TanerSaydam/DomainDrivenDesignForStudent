using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Orders.ValueObjects;

namespace DomainDrivenDesign.Domain.Orders;

public sealed class Order : Entity, IAggregate
{
    private Order()
    {
        Date = default!;
    }

    public static Order Create(OrderDate date, List<OrderItem> items)
    {
        var order = new Order();
        order.SetDate(date);
        order.SetItems(items);
        return order;
    }

    private readonly List<OrderItem> _orderItems = new();
    public OrderDate Date { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _orderItems;

    public void SetDate(OrderDate date)
    {
        Date = date;
    }

    public void SetItems(List<OrderItem> items)
    {
        _orderItems.Clear();
        _orderItems.AddRange(items);
    }
}