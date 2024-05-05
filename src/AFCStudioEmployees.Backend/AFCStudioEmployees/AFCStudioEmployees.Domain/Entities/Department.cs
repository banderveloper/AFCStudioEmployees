using System.Text.Json.Serialization;

namespace AFCStudioEmployees.Domain.Entities;

/// <summary>
/// Job department
/// </summary>
public class Department : BaseEntity
{
    public string Name { get; set; }
    
    [JsonIgnore]
    public IEnumerable<Employee> Employees { get; set; }
}