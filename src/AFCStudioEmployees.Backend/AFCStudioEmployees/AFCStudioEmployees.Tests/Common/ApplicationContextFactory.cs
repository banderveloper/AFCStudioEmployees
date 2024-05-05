using AFCStudioEmployees.Domain.Entities;
using AFCStudioEmployees.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Tests.Common;

/// <summary>
/// Entity framework DB context factory with initialized entities
/// </summary>
public class ApplicationContextFactory
{
    // Test names of departments
    public static string FirstDepartmentName = "First";
    public static string SecondDepartmentName = "Second";
    public static string ThirdDepartmentName = "Third";

    // Test jobs
    public static Job FirstJob = new() { Id = 50, Name = "Manager", Salary = 16000 };
    public static Job SecondJob = new() { Id = 51, Name = "Cleaner", Salary = 12000 };
    public static Job ThirdJob = new() { Id = 52, Name = "Director", Salary = 54000 };

    /// <summary>
    /// Create configurated and filled database context
    /// </summary>
    /// <returns>Configurated and filled database context</returns>
    public static ApplicationDbContext Create()
    {
        // InMemory database options
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .Options;

        // Create database
        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();

        // Fill database with departments
        context.Departments.AddRange(
            new Department { Id = 50, Name = FirstDepartmentName },
            new Department { Id = 51, Name = SecondDepartmentName },
            new Department { Id = 52, Name = ThirdDepartmentName }
        );

        // Fill database with jobs
        context.Jobs.AddRange(FirstJob, SecondJob, ThirdJob);

        // Fill database with employees
        context.Employees.AddRange(
            new Employee
            {
                Id = 50,
                LastName = "First",
                FirstName = "First",
                MiddleName = "First",
                JobId = 1,
                DepartmentId = 1
            },
            new Employee
            {
                Id = 51,
                LastName = "Second",
                FirstName = "Second",
                MiddleName = "Second",
                JobId = 2,
                DepartmentId = 1
            },
            new Employee
            {
                Id = 52,
                LastName = "Third",
                FirstName = "Third",
                MiddleName = "Third",
                JobId = 1,
                DepartmentId = 2
            });

        context.SaveChanges();
        return context;
    }

    /// <summary>
    /// Drop database from given context
    /// </summary>
    /// <param name="context">Context to drop database</param>
    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}