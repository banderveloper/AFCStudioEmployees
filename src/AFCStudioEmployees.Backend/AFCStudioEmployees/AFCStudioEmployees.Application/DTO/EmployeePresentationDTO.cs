namespace AFCStudioEmployees.Application.DTO;

public class EmployeePresentationDTO
{
    public long EmployeeId { get; set; }
    
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    
    public DateTime BirthDate { get; set; }
    public DateTime EmployeeInviteTime { get; set; }
    public decimal EmployeeSalary { get; set; }
    
    public long DepartmentId { get; set; }
}