using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Dtos;
using DomainDrivenDesign.Domain.Products;

namespace DomainDrivenDesign.Application.Products;

public sealed class ProductDto : AuditDto
{
    public string Name { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
    public int Stock { get; set; }
    public decimal Price { get; set; }
}

public static class ProductExtensions
{
    public static IQueryable<ProductDto> MapTo(this IQueryable<AuditQueryableDto<Product>> query, IQueryable<Category> categories)
    {
        var res = query.AsQueryable()
            .LeftJoin(categories, m => m.Entity.CategoryId, m => m.Id, (e, category)
                => new { Entity = e.Entity, CreatedUser = e.CreatedUser, UpdatedUser = e.UpdatedUser, Category = category })
            .Select(s => new ProductDto()
            {
                Id = s.Entity.Id,
                Name = s.Entity.Name.Value,
                CategoryId = s.Entity.CategoryId,
                CategoryName = s.Category!.Name.Value,
                Stock = s.Entity.Stock.Value,
                Price = s.Entity.Price.Value,
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

