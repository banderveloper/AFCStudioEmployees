using AFCStudioEmployees.Application.Interfaces;
using AFCStudioEmployees.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Commands;

public class CreateDepartmentCommand : IRequest<Result<Department>>
{
    public string DepartmentName { get; set; }
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<Department>>
{
    private readonly IApplicationDbContext _context;

    public CreateDepartmentCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<Department>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        // set department name to lowercase for searching
        var searchDepartmentName = request.DepartmentName.ToLower();

        // look for existing department by name
        var existingDepartment = await _context.Departments.FirstOrDefaultAsync(department =>
            department.Name.ToLower().Equals(searchDepartmentName), cancellationToken);

        // if department exists - error
        if (existingDepartment is not null)
            return Result<Department>.Error(ErrorCode.DepartmentAlreadyExists);

        // if not exists - create
        var newDepartment = new Department { Name = request.DepartmentName };
        _context.Departments.Add(newDepartment);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Department>.Success(newDepartment);
    }
}