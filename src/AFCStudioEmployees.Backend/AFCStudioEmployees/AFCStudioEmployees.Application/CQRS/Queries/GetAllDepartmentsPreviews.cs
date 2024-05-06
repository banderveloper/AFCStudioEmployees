using AFCStudioEmployees.Application.DTO;
using AFCStudioEmployees.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Queries;

public class GetAllDepartmentsPreviewsQuery : IRequest<Result<IEnumerable<DepartmentPreviewDTO>>>
{
}

public class GetAllDepartmentsPreviewsQueryHandler : IRequestHandler<GetAllDepartmentsPreviewsQuery,
    Result<IEnumerable<DepartmentPreviewDTO>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllDepartmentsPreviewsQueryHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<IEnumerable<DepartmentPreviewDTO>>> Handle(GetAllDepartmentsPreviewsQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Departments
            .Select(department => new DepartmentPreviewDTO
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name
            }).ToListAsync(cancellationToken);

        return Result<IEnumerable<DepartmentPreviewDTO>>.Success(result);
    }
}