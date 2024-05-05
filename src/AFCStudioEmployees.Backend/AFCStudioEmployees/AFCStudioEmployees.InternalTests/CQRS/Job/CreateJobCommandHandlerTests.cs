using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Commands;
using AFCStudioEmployees.InternalTests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AFCStudioEmployees.InternalTests.CQRS.Job;

/// <summary>
/// Unit tests for CQRS command of create job
/// </summary>
public class CreateJobCommandHandlerTests : BaseTest
{
    /// <summary>
    /// Successfull creation of job
    /// </summary>
    [Fact]
    public async Task Success()
    {
        // Act
        var handler = new CreateJobCommandHandler(Context);

        // Arrange
        var createdJobResult = await handler.Handle(new CreateJobCommand { JobName = "New job", Salary = 15632 },
            CancellationToken.None);

        // Assert
        Assert.True(createdJobResult.Succeed);
        Assert.Null(createdJobResult.ErrorCode);

        Assert.NotNull(await Context.Jobs.FirstOrDefaultAsync(job =>
            job.Id == createdJobResult.Data.Id &&
            job.Name == createdJobResult.Data.Name &&
            job.Salary == createdJobResult.Data.Salary));
    }

    /// <summary>
    /// Failed creation of job with existing name
    /// </summary>
    [Fact]
    public async Task FailOnExistingName()
    {
        // Act
        var handler = new CreateJobCommandHandler(Context);

        // Arrange
        var createdJobResult = await handler.Handle(
            new CreateJobCommand { JobName = ApplicationContextFactory.FirstJob.Name, Salary = 15632 },
            CancellationToken.None);

        // Assert
        Assert.False(createdJobResult.Succeed);
        Assert.Equal(createdJobResult.ErrorCode, ErrorCode.JobAlreadyExists);
    }

    /// <summary>
    /// Failed creation of job with negative salary
    /// </summary>
    [Fact]
    public async Task FailOnNegativeSalary()
    {
        // Act
        var handler = new CreateJobCommandHandler(Context);

        // Arrange
        var createdJobResult = await handler.Handle(
            new CreateJobCommand { JobName = "Unique name", Salary = -500 },
            CancellationToken.None);

        // Assert
        Assert.False(createdJobResult.Succeed);
        Assert.Equal(createdJobResult.ErrorCode, ErrorCode.JobSalaryLessThanZero);
    }
    
    /// <summary>
    /// Failed creation of job with negative salary
    /// </summary>
    [Fact]
    public async Task FailOnEmptyJobName()
    {
        // Act
        var handler = new CreateJobCommandHandler(Context);

        // Arrange
        var createdJobResult = await handler.Handle(
            new CreateJobCommand { JobName = "", Salary = 5000 },
            CancellationToken.None);

        // Assert
        Assert.False(createdJobResult.Succeed);
        Assert.Equal(createdJobResult.ErrorCode, ErrorCode.JobNameEmpty);
    }
}