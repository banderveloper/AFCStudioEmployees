using AFCStudioEmployees.Application.Interfaces;
using AFCStudioEmployees.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AFCStudioEmployees.Application.CQRS.Commands;

public class CreateJobCommand : IRequest<Result<Job>>
{
    public string JobName { get; set; }
    public decimal Salary { get; set; }
}

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<Job>>
{
    private readonly IApplicationDbContext _context;

    public CreateJobCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Result<Job>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.JobName))
            return Result<Job>.Error(ErrorCode.JobNameEmpty);

        if (request.Salary < 0)
            return Result<Job>.Error(ErrorCode.JobSalaryLessThanZero);

        // lowered job name to search
        var searchJobName = request.JobName.ToLower();

        // try to find existing job by name
        var existingJob = await _context.Jobs
            .FirstOrDefaultAsync(job => job.Name.ToLower().Equals(searchJobName), cancellationToken);

        // if job by name already exists
        if (existingJob is not null)
            return Result<Job>.Error(ErrorCode.JobAlreadyExists);
        
        // if ok - create
        var newJob = new Job { Name = request.JobName, Salary = request.Salary };
        _context.Jobs.Add(newJob);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Job>.Success(newJob);
    }
}