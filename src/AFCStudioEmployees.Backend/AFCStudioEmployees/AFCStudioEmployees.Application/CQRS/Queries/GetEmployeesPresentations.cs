using AFCStudioEmployees.Application.DTO;
using AFCStudioEmployees.Application.Extensions;
using AFCStudioEmployees.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Queries;

public class GetEmployeesPresentationsQuery : IRequest<Result<IEnumerable<EmployeePresentationDTO>>>
{
    public string SortProperty { get; set; }
    public string SearchTerm { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 0;
}

/// <summary>
/// Get employees with pagination, filtering and ordering
/// </summary>
public class GetEmployeesPresentationsQueryHandler : IRequestHandler<GetEmployeesPresentationsQuery,
    Result<IEnumerable<EmployeePresentationDTO>>>
{
    private IApplicationDbContext _context;

    public GetEmployeesPresentationsQueryHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<IEnumerable<EmployeePresentationDTO>>> Handle(GetEmployeesPresentationsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.PageSize < 1)
            return Result<IEnumerable<EmployeePresentationDTO>>.Error(ErrorCode.InvalidPagination);

        // If sort property is not set - order by first found property
        if (string.IsNullOrWhiteSpace(request.SortProperty))
            request.SortProperty = typeof(EmployeePresentationDTO).GetProperties().First().Name;

        // Sort property name is given by string. Check whether datatype has property with this name
        if (!typeof(EmployeePresentationDTO).HasProperty(request.SortProperty))
            return Result<IEnumerable<EmployeePresentationDTO>>.Error(ErrorCode.PropertyNameNotFound);

        // Form query and joins
        var query = _context.Employees
            .Include(employee => employee.Department)
            .Include(employee => employee.Job)
            .AsQueryable();

        // Search by filter
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            request.SearchTerm = request.SearchTerm.ToLower();

            query = query.Where(employee =>
                EF.Functions.Like(employee.LastName.ToLower(), $"%{request.SearchTerm}%") ||
                EF.Functions.Like(employee.FirstName.ToLower(), $"%{request.SearchTerm}%") ||
                EF.Functions.Like(employee.MiddleName.ToLower(), $"%{request.SearchTerm}%") ||
                EF.Functions.Like(employee.Job.Name.ToLower(), $"%{request.SearchTerm}%")
            );
        }

        // Sort and pagination
        query = query
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize);

        // Convert to DTO and order
        var result = await query.Select(employee => new EmployeePresentationDTO
            {
                EmployeeId = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                BirthDate = employee.Birthdate,
                DepartmentId = employee.DepartmentId,
                EmployeeSalary = employee.Job.Salary,
                EmployeeInviteTime = employee.CreatedAt
            })
            .OrderByProperty(request.SortProperty)
            .ToListAsync(cancellationToken);

        return Result<IEnumerable<EmployeePresentationDTO>>.Success(result);
    }
}