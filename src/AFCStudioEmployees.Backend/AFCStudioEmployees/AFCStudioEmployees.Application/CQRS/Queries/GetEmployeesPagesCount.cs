using AFCStudioEmployees.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Queries;

public class GetEmployeesPagesCountQuery : IRequest<Result<int>>
{
    public int EmployeesPerPage { get; set; }
}

public class GetEmployeesPagesCountQueryHandler : IRequestHandler<GetEmployeesPagesCountQuery, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeesPagesCountQueryHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<int>> Handle(GetEmployeesPagesCountQuery request, CancellationToken cancellationToken)
    {
        if (request.EmployeesPerPage < 1)
            return Result<int>.Error(ErrorCode.InvalidPagination);

        double totalEmployeesCount = await _context.Employees.CountAsync(cancellationToken);

        int pagesCount = (int)Math.Ceiling(totalEmployeesCount / request.EmployeesPerPage);

        return Result<int>.Success(pagesCount);
    }
}
