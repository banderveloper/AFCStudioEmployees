namespace AFCStudioEmployees.WebAPI.Models;

public class GetEmployeesRequest
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 10;
    public string? Search { get; set; }
    public string? SortBy { get; set; }
}