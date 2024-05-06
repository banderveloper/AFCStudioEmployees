using AFCStudioEmployees.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AFCStudioEmployees.Persistence.EntityTypeConfigurations;

/// <summary>
/// Entity framework Employee type configuration 
/// </summary>
public class EmployeeTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        // Employee's foreign key to job
        builder
            .HasOne<Job>(employee => employee.Job)
            .WithMany(job => job.Employees)
            .HasForeignKey(employee => employee.JobId);

        // Employee's foreign key to department
        builder
            .HasOne<Department>(employee => employee.Department)
            .WithMany(department => department.Employees)
            .HasForeignKey(employee => employee.DepartmentId);
    }
}