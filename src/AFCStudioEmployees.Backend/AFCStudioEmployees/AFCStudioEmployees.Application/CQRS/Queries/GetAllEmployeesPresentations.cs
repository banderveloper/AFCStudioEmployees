using AFCStudioEmployees.Application.DTO;
using AFCStudioEmployees.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Queries;

public class GetAllEmployeesPresentationsQuery : IRequest<Result<IEnumerable<EmployeePresentationDTO>>>
{
}

public class GetAllEmployeesPresentationsQueryHandler : IRequestHandler<GetAllEmployeesPresentationsQuery,
    Result<IEnumerable<EmployeePresentationDTO>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllEmployeesPresentationsQueryHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<IEnumerable<EmployeePresentationDTO>>> Handle(GetAllEmployeesPresentationsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _context.Employees
            .Include(employee => employee.Department)
            .Include(employee => employee.Job)
            .Select(employee => new EmployeePresentationDTO
            {
                EmployeeId = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                BirthDate = employee.Birthdate,
                DepartmentId = employee.DepartmentId,
                EmployeeSalary = employee.Job.Salary,
                EmployeeInviteTime = employee.CreatedAt
            }).ToListAsync(cancellationToken);

        return Result<IEnumerable<EmployeePresentationDTO>>.Success(result);
    }
}