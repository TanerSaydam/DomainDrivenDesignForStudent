using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Shared;

namespace DomainDrivenDesign.Domain.Categories;

public sealed class Category : Entity
{
    private Category()
    {
        Name = default!;
    }
    private Category(Name name)
    {
        Name = name;
    }
    public Name Name { get; private set; }

    public static Category Create(Name name)
    {
        return new Category(name);
    }

    public void SetName(Name name)
    {
        Name = name;
    }
}