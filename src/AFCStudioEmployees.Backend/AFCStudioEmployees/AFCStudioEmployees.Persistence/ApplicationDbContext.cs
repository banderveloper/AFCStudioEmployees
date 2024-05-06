using AFCStudioEmployees.Application.Interfaces;
using AFCStudioEmployees.Domain.Entities;
using AFCStudioEmployees.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Persistence;

/// <summary>
/// Core entity framemework database context
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply entity type configurations
        modelBuilder.ApplyConfiguration(new DepartmentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeTypeConfiguration());

        // Add core departments to db
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Headhunters" },
            new Department { Id = 2, Name = "Programmers" },
            new Department { Id = 3, Name = "Designers" }
        );

        // Add core jobs to db
        modelBuilder.Entity<Job>().HasData(
            new Job { Id = 1, Name = "Junior backend developer", Salary = 16000 },
            new Job { Id = 2, Name = "Senior backend developer", Salary = 68450 },
            new Job { Id = 3, Name = "Junior designer", Salary = 12500 },
            new Job { Id = 4, Name = "Senior designer", Salary = 54340 },
            new Job { Id = 5, Name = "QA engineer", Salary = 21000 },
            new Job { Id = 6, Name = "Director", Salary = 100000 },
            new Job { Id = 7, Name = "Account manager", Salary = 32000 },
            new Job { Id = 8, Name = "Office manager", Salary = 16000 }
        );

        // Add core employees to db
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1, 
                LastName = "Kalnitskiy", 
                FirstName = "Nikita",
                MiddleName = "Andreevich",
                Birthdate = new DateTime(2002, 4, 3),
                DepartmentId = 2,
                JobId = 1
            },
            new Employee
            {
                Id = 2, 
                LastName = "Popov", 
                FirstName = "Oleg",
                MiddleName = "Olegovich",
                Birthdate = new DateTime(1998, 5, 1),
                DepartmentId = 2,
                JobId = 2
            },
            new Employee
            {
                Id = 3, 
                LastName = "Zakladna", 
                FirstName = "Darina",
                MiddleName = "Yurievna",
                Birthdate = new DateTime(2000, 6, 6),
                DepartmentId = 3,
                JobId = 3
            }
        );
    }
}