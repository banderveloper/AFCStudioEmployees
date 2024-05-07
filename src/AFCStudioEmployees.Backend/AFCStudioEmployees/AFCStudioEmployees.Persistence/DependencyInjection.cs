using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.Configurations;
using AFCStudioEmployees.Application.Exceptions;
using AFCStudioEmployees.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AFCStudioEmployees.Persistence;

/// <summary>
/// Persistence level extensions methods
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Inject persistence layer dependencies to main IOC
    /// </summary>
    /// <param name="services">Link to main IOC</param>
    /// <param name="environmentName">Name of runtime environment (Development/DockerDevelopment/Preproduction...)</param>
    /// <returns>IOC with completed injections</returns>
    /// <exception cref="Exception">Throws in case of unknown environment name</exception>
    public static IServiceCollection AddPersistence(this IServiceCollection services, string environmentName)
    {
        var scope = services.BuildServiceProvider().CreateScope();

        // Get database configurations from configurations
        var databaseConfiguration = scope.ServiceProvider.GetRequiredService<DatabaseConfiguration>();

        // Inject database context
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            // If it is no appsettings for given environment - don't run server
            if (string.IsNullOrEmpty(databaseConfiguration.ConnectionStringPattern))
                throw new LoggedFatalException($"Configuration for environment '{environmentName}' not found!");
            
            // ConnectionStringPattern looks like ="username={0}, password={1}"
            var filledConnectionString = string.Format(databaseConfiguration.ConnectionStringPattern,
                databaseConfiguration.Username, databaseConfiguration.Password);

            // If development env - use sqlite, if more serious - docker containered postgres
            switch (environmentName)
            {
                case EnvironmentState.Development:
                    options.UseSqlite(filledConnectionString);
                    break;
                case EnvironmentState.DockerDevelopment:
                case EnvironmentState.Preproduction:
                case EnvironmentState.Production:
                    options.UseNpgsql(filledConnectionString);
                    break;
                default:
                    throw new LoggedFatalException($"Unknown environment '{environmentName}'");
            }

            // No ef caching, increases EF perfomance
            // in Update ef quieries required to use .AsTracking() for working
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        // Bind interface and earlier injected app db context
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}