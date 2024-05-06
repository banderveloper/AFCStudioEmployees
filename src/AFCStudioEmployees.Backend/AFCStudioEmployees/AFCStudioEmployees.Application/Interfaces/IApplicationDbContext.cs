using AFCStudioEmployees.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Department> Departments { get; set; }
    DbSet<Job> Jobs { get; set; }
    DbSet<Employee> Employees { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}