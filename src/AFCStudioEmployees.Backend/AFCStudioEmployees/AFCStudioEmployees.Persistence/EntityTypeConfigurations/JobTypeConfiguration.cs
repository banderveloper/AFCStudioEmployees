using AFCStudioEmployees.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AFCStudioEmployees.Persistence.EntityTypeConfigurations;

/// <summary>
/// Entity framework Job type configuration 
/// </summary>
public class JobTypeConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasIndex(job => job.Name).IsUnique();
    }
}