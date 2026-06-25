using DomainDrivenDesign.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesign.Infrastructure.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.OwnsOne(o => o.Date, date =>
        {
            date.Property(d => d.Value)
                .HasColumnName("Date");
        });
        builder.OwnsMany(o => o.Items);
    }
}
