using AFCStudioEmployees.Application.Interfaces;
using AFCStudioEmployees.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Commands;

public class UpdateEmployeeCommand : IRequest<Result<Employee>>
{
    public long EmployeeId { get; set; }

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime BirthDate { get; set; }

    public long JobId { get; set; }
    public long DepartmentId { get; set; }
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Result<Employee>>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<Employee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Check department existing
        var existingDepartment = await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == request.DepartmentId, cancellationToken);
        if (existingDepartment is null)
            return Result<Employee>.Error(ErrorCode.DepartmentNotFound);

        // Check job existing
        var existingJob = await _context.Jobs
            .FirstOrDefaultAsync(j => j.Id == request.JobId, cancellationToken);
        if (existingJob is null)
            return Result<Employee>.Error(ErrorCode.JobNotFound);

        // Get existing employee
        var existingEmployee = await _context.Employees.AsTracking()
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (existingEmployee is null)
            return Result<Employee>.Error(ErrorCode.EmployeeNotFound);

        existingEmployee.LastName = request.LastName;
        existingEmployee.FirstName = request.FirstName;
        existingEmployee.MiddleName = request.MiddleName;
        existingEmployee.Birthdate = request.BirthDate;
        existingEmployee.JobId = request.JobId;
        existingEmployee.DepartmentId = request.DepartmentId;

        _context.Employees.Update(existingEmployee);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Employee>.Success(existingEmployee);
    }
}