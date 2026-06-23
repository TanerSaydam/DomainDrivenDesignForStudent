using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Categories;
using DomainDrivenDesign.Domain.Products;
using DomainDrivenDesign.Domain.Users;
using DomainDrivenDesign.Infrastructure.Context;
using DomainDrivenDesign.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesign.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            string con = configuration.GetConnectionString("SqlServer")!;
            opt.UseSqlServer(con);
        });
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequiredLength = 1;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}