using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Infrastructure.Context;

namespace DomainDrivenDesign.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}