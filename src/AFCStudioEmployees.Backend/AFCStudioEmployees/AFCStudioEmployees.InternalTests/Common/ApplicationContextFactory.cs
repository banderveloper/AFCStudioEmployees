using AFCStudioEmployees.Domain.Entities;
using AFCStudioEmployees.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.InternalTests.Common;

/// <summary>
/// Entity framework DB context factory with initialized entities
/// </summary>
public class ApplicationContextFactory
{
    // Test departments
    public static Department FirstDepartment = new() { Id = 50, Name = "First" };
    public static Department SecondDepartment = new() { Id = 51, Name = "Second" };
    public static Department ThirdDepartment = new() { Id = 52, Name = "Third" };

    // Test jobs
    public static Job FirstJob = new() { Id = 50, Name = "Manager", Salary = 16000 };
    public static Job SecondJob = new() { Id = 51, Name = "Cleaner", Salary = 12000 };
    public static Job ThirdJob = new() { Id = 52, Name = "Director", Salary = 54000 };

    // Test employees
    public static Employee FirstEmployee = new()
        { Id = 50, LastName = "First", FirstName = "First", MiddleName = "First", JobId = 1, DepartmentId = 1 };

    public static Employee SecondEmployee = new()
        { Id = 51, LastName = "Second", FirstName = "Second", MiddleName = "Second", JobId = 2, DepartmentId = 1 };

    public static Employee ThirdEmployee = new Employee
        { Id = 52, LastName = "Third", FirstName = "Third", MiddleName = "Third", JobId = 1, DepartmentId = 2 };

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
        context.Departments.AddRange(FirstDepartment, SecondDepartment, ThirdDepartment);

        // Fill database with jobs
        context.Jobs.AddRange(FirstJob, SecondJob, ThirdJob);

        // Fill database with employees
        context.Employees.AddRange(FirstEmployee, SecondEmployee, ThirdEmployee);

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