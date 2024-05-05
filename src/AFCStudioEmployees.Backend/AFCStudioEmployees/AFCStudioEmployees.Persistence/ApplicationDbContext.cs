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
        modelBuilder.ApplyConfiguration(new DepartmentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeTypeConfiguration());
    }
}