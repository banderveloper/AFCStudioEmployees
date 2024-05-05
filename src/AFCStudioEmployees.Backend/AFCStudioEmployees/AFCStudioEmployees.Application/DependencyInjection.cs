using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AFCStudioEmployees.Application;

/// <summary>
/// Application layer DI injector
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Injecting MediatR for CQRS
        services.AddMediatR(config => { config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()); });

        return services;
    }
}