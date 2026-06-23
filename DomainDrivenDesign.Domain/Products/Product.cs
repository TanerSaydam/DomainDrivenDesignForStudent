using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Products.ValuObjects;
using DomainDrivenDesign.Domain.Shared;

namespace DomainDrivenDesign.Domain.Products;

public sealed class Product : Entity
{
    private Product()
    {
        Name = default!;
        Stock = default!;
        Price = default!;
        CategoryId = default!;
    }

    public Product(Name name, Stock stock, Price price, Guid categoryId)
    {
        Name = name;
        Stock = stock;
        Price = price;
        CategoryId = categoryId;
    }

    public Name Name { get; private set; }
    public Stock Stock { get; private set; }
    public Price Price { get; private set; }
    public Guid CategoryId { get; private set; }

    public void SetName(Name name)
    {
        Name = name;
    }

    public void SetStock(Stock stock)
    {
        Stock = stock;
    }

    public void SetPrice(Price price)
    {
        Price = price;
    }

    public void SetCategory(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}