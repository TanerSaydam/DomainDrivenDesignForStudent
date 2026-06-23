using DomainDrivenDesign.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesign.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.OwnsOne(p => p.Name, nav =>
        {
            nav.Property(i => i.Value).HasColumnType("varchar(100)").HasColumnName("Name");
        });
        builder.OwnsOne(p => p.Stock, nav =>
        {
            nav.Property(i => i.Value).HasColumnName("Stock");
        });
        builder.OwnsOne(p => p.Price, nav =>
        {
            nav.Property(i => i.Value).HasColumnName("Price").HasColumnType("money");
        });
    }
}