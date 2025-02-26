using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register other services
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}