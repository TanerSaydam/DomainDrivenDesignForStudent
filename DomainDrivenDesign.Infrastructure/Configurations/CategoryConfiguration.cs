using DomainDrivenDesign.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesign.Infrastructure.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.OwnsOne(p => p.Name, nav =>
        {
            nav.Property(i => i.Value).HasColumnType("varchar(100)").HasColumnName("Name");
        });
    }
}