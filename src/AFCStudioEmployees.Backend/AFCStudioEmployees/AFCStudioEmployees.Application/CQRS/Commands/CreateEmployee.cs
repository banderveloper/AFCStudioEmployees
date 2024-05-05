using AFCStudioEmployees.Application.Interfaces;
using AFCStudioEmployees.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Commands;

public class CreateEmployeeCommand : IRequest<Result<Employee>>
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }

    public long JobId { get; set; }
    public long DepartmentId { get; set; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<Employee>>
{
    private readonly IApplicationDbContext _context;

    public CreateEmployeeCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<Employee>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
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
        
        // if ok - create
        var newEmployee = new Employee
        {
            LastName = request.LastName,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,

            DepartmentId = request.DepartmentId,
            JobId = request.JobId
        };

        _context.Employees.Add(newEmployee);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Employee>.Success(newEmployee);
    }
}