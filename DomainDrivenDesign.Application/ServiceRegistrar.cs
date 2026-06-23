using DomainDrivenDesign.Application.Behaviors;
using DomainDrivenDesign.Application.Categories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenDesign.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfr =>
        {
            cfr.RegisterServicesFromAssembly(typeof(CategoryCreateCommand).Assembly);
            cfr.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfr.AddBehavior(typeof(TCKontrolBehavior));
        });

        services.AddValidatorsFromAssembly(typeof(ServiceRegistrar).Assembly);
        return services;
    }
}
