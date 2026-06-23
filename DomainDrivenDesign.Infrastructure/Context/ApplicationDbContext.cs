using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.Context;

internal sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");

        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
        modelBuilder.Ignore<IdentityRole<Guid>>();
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();

    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();

        string id = "b9320d95-a614-4baf-132b-08dec71c956f";
        Guid guidId = Guid.Parse(id);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedDate)
                    .CurrentValue = DateTimeOffset.Now;
                entry.Property(p => p.CreatedUserId)
                    .CurrentValue = guidId;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeletedDate)
                    .CurrentValue = DateTimeOffset.Now;
                    entry.Property(p => p.DeletedUserId)
                    .CurrentValue = guidId;
                }
                else
                {
                    entry.Property(p => p.UpdatedDate)
                        .CurrentValue = DateTimeOffset.Now;
                    entry.Property(p => p.UpdatedUserId)
                    .CurrentValue = guidId;
                }
            }

            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("Db'den direkt silme işlemi yapamazsınız");
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}