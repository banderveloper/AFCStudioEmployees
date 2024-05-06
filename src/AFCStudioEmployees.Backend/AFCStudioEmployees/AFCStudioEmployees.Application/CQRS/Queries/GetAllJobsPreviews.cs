using AFCStudioEmployees.Application.DTO;
using AFCStudioEmployees.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Queries;

public class GetAllJobsPreviewsQuery : IRequest<Result<IEnumerable<JobPreviewDTO>>>
{
}

public class GetAllJobsPreviewsQueryHandler : IRequestHandler<GetAllJobsPreviewsQuery, Result<IEnumerable<JobPreviewDTO>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllJobsPreviewsQueryHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<IEnumerable<JobPreviewDTO>>> Handle(GetAllJobsPreviewsQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Jobs.Select(job => new JobPreviewDTO
        {
            JobId = job.Id,
            JobName = job.Name
        }).ToListAsync(cancellationToken);

        return Result<IEnumerable<JobPreviewDTO>>.Success(result);
    }
}