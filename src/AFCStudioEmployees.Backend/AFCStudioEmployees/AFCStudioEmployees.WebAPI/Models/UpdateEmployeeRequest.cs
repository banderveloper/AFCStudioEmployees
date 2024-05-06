namespace AFCStudioEmployees.WebAPI.Models;

public class UpdateEmployeeRequest
{
    public long EmployeeId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime BirthDate { get; set; }

    public long JobId { get; set; }
    public long DepartmentId { get; set; }
}