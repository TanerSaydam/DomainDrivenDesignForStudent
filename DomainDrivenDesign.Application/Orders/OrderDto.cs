using DomainDrivenDesign.Domain.Dtos;
using DomainDrivenDesign.Domain.Orders;
using DomainDrivenDesign.Domain.Products;

namespace DomainDrivenDesign.Application.Orders;

public sealed class OrderDto : AuditDto
{
    public DateOnly Date { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public sealed class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public static class OrderExtensions
{
    public static IQueryable<OrderDto> MapTo(this IQueryable<AuditQueryableDto<Order>> query, IQueryable<Product> products)
    {
        var res = query.AsQueryable()
            .Select(s => new OrderDto()
            {
                Id = s.Entity.Id,
                Date = s.Entity.Date.Value,
                Items = s.Entity.Items
                    .LeftJoin(products, m => m.ProductId, m => m.Id, (item, product) => new { item, product })
                    .Select(i => new OrderItemDto
                    {
                        ProductId = i.item.ProductId,
                        ProductName = i.product!.Name.Value,
                        Price = i.item.Price,
                        Quantity = i.item.Quantity,
                    }).ToList(),
                CreatedDate = s.Entity.CreatedDate,
                CreatedUserId = s.Entity.CreatedUserId,
                CreatedUserName = s.CreatedUser!.FullName,
                UpdatedDate = s.Entity.UpdatedDate,
                UpdatedUserId = s.Entity.UpdatedUserId,
                UpdatedUserName = s.Entity.UpdatedUserId == null ? null : s.UpdatedUser!.FullName
            });
        return res;
    }
}