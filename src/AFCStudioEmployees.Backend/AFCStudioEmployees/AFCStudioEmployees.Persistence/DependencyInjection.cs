using Microsoft.Extensions.DependencyInjection;

namespace AFCStudioEmployees.Persistence;

/// <summary>
/// Persistence level extensions methods
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {

        return services;
    }
}