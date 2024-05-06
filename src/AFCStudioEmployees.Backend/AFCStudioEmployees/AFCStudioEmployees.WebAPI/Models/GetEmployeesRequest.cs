using System.ComponentModel.DataAnnotations;

namespace AFCStudioEmployees.WebAPI.Models;

public class GetEmployeesRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must be positive")] 
    public int Page { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must be positive")]
    public int Size { get; set; } = 10;

    public string? Search { get; set; }
    public string? SortBy { get; set; }
}