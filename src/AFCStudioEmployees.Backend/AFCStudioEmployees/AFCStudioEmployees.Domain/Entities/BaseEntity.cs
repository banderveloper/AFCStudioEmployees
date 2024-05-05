using System.ComponentModel.DataAnnotations;

namespace AFCStudioEmployees.Domain.Entities;

/// <summary>
/// Parent for database entities
/// </summary>
public class BaseEntity
{
    [Key]
    public long Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}