using System.ComponentModel.DataAnnotations;

namespace AFCStudioEmployees.WebAPI.Models;

public class UpdateEmployeeRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Employee id must be positive")]
    public long EmployeeId { get; set; }

    [Required(ErrorMessage = "Employee last name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Employee first name is required")]
    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Employee birth date is required")]
    public DateTime BirthDate { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Job id must be positive")]
    public long JobId { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Department id must be positive")]
    public long DepartmentId { get; set; }
}