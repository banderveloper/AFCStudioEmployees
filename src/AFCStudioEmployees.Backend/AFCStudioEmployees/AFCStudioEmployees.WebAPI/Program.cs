using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.Converters;
using AFCStudioEmployees.Persistence;
using AFCStudioEmployees.WebAPI;
using AFCStudioEmployees.WebAPI.Middleware;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomConfiguration();

// Inject other layers
builder.Services
    .AddApplication()
    .AddPersistence(builder.Environment.EnvironmentName.ToLower());

var scope = builder.Services.BuildServiceProvider().CreateScope();

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

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // ErrorCode enum int value to snake_case_string in response (ex: not 1, but username_already_exists)
    options.JsonSerializerOptions.Converters.Add(new SnakeCaseStringEnumConverter<ErrorCode>());
});

// Initialize database if it is not exists
var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
DatabaseInitializer.Initialize(applicationDbContext);

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseCors("AllowAll");

app.MapGet("/time", () => DateTime.UtcNow);
app.MapControllerRoute(name: "default", pattern: "{controller}/{action}");

Console.WriteLine("Server started");
app.Run();