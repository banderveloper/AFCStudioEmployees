using AFCStudioEmployees.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AFCStudioEmployees.Persistence.EntityTypeConfigurations;

/// <summary>
/// Entity framework Department type configuration 
/// </summary>
public class DepartmentTypeConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasIndex(department => department.Name).IsUnique();
    }
}