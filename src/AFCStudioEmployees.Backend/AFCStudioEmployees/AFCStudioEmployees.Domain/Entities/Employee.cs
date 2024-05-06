using System.Text.Json.Serialization;

namespace AFCStudioEmployees.Domain.Entities;

public class Employee : BaseEntity
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime Birthdate { get; set; }

    public long DepartmentId { get; set; }
    [JsonIgnore] public Department Department { get; set; }

    public long JobId { get; set; }
    [JsonIgnore] public Job Job { get; set; }
}