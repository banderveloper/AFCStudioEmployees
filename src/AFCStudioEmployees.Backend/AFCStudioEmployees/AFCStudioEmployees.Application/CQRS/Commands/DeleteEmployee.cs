using AFCStudioEmployees.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Commands;

public class DeleteEmployeeCommand : IRequest<Result<None>>
{
    public long EmployeeId { get; set; }
}

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<None>>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeCommandHandler(IApplicationDbContext context)
        => _context = context;
    
    public async Task<Result<None>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var existingEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (existingEmployee is null)
            return Result<None>.Error(ErrorCode.EmployeeNotFound);

        _context.Employees.Remove(existingEmployee);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<None>.Success();
    }
}