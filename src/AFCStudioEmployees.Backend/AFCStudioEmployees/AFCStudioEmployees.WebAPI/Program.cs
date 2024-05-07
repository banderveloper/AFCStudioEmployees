using System.Reflection;
using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.Converters;
using AFCStudioEmployees.Application.Extensions;
using AFCStudioEmployees.Persistence;
using AFCStudioEmployees.WebAPI;
using AFCStudioEmployees.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using static System.String;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

// Logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .WriteTo.File("logs.log")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Listening ports (problems with docker without it)
builder.WebHost.UseUrls("http://*:80", "http://*:5035");

// Remove another logging providers
builder.Logging.ClearProviders();

// Serilogger
builder.Services.AddSerilog();

Log.Information($"Starting server with '{builder.Environment.EnvironmentName}' environment...");

builder.AddCustomConfiguration();

// Inject other layers
builder.Services
    .AddApplication()
    .AddPersistence(builder.Environment.EnvironmentName);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("*", "null", "http://localhost:5000", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        // ErrorCode enum int value to snake_case_string in response (ex: not 1, but username_already_exists)
        options.JsonSerializerOptions.Converters.Add(new SnakeCaseStringEnumConverter<ErrorCode>());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // Change default 422 behaviour
        options.InvalidModelStateResponseFactory = context =>
        {
            // Get list of validation errors in format {propName: [error1, error2]}
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Any())
                .ToDictionary(
                    kvp => kvp.Key.ToLowerFirstLetter(),
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            // Generate server response
            var result = new UnprocessableEntityObjectResult(new Result<Dictionary<string, string[]>>
            {
                ErrorCode = ErrorCode.InvalidModel,
                Data = errors
            });
            result.ContentTypes.Add("application/json");

            return result;
        };
    });

// Swagger
builder.Services.AddSwaggerGen(config =>
{
    // xml comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

// Initialize database if it is not exists
var scope = builder.Services.BuildServiceProvider().CreateScope();
var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
DatabaseInitializer.Initialize(applicationDbContext);

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseCors("AllowAll");

// If development environment
if (builder.Environment.EnvironmentName is EnvironmentState.Development or EnvironmentState.DockerDevelopment)
{
    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "AFCStudioEmployees");
    });

    // Request logging
    app.UseSerilogRequestLogging();
}

app.MapGet("/time", () => DateTime.UtcNow);
app.MapControllerRoute(name: "default", pattern: "{controller}/{action}");

Log.Information($"Server started on '{app.Environment.EnvironmentName}' environment!");
app.Run();