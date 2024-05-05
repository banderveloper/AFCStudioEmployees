using Microsoft.Extensions.DependencyInjection;

namespace AFCStudioEmployees.Application;

/// <summary>
/// Application layer DI injector
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // ...

        return services;
    }
}