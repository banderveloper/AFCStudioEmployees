using System.Text.Json.Serialization;

namespace AFCStudioEmployees.Domain.Entities;

/// <summary>
/// Job post
/// </summary>
public class Job : BaseEntity
{
    public string Name { get; set; }
    public double Salary { get; set; }
    
    [JsonIgnore]
    public IEnumerable<Employee> Employees { get; set; }
}