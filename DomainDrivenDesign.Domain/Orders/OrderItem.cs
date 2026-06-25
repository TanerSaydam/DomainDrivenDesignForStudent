namespace DomainDrivenDesign.Domain.Orders;

public sealed record OrderItem(Guid ProductId, int Quantity, decimal Price);
