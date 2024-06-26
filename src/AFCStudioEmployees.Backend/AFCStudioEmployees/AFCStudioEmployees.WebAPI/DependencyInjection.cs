﻿using AFCStudioEmployees.Application.Configurations;
using Microsoft.Extensions.Options;

namespace AFCStudioEmployees.WebAPI;

public static class DependencyInjection
{
    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.ConfigurationKey));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<DatabaseConfiguration>>().Value);
    }
}