using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Infrastructure.Context;

namespace DomainDrivenDesign.Infrastructure.Repositories;

internal sealed class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}