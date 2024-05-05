using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Commands;
using AFCStudioEmployees.InternalTests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AFCStudioEmployees.InternalTests.CQRS.Employee;

public class CreateEmployeeCommandHandlerTests : BaseTest
{
    [Fact]
    public async Task Success()
    {
        // Arrange
        var handler = new CreateEmployeeCommandHandler(Context);

        // Act
        var createEmployeeResult = await handler.Handle(new CreateEmployeeCommand
        {
            LastName = "Test",
            FirstName = "Test",
            MiddleName = "Test",
            DepartmentId = ApplicationContextFactory.FirstDepartment.Id,
            JobId = ApplicationContextFactory.FirstJob.Id
        }, CancellationToken.None);

        // Assert
        Assert.True(createEmployeeResult.Succeed);
        Assert.Null(createEmployeeResult.ErrorCode);

        Assert.NotNull(await Context.Employees.FirstOrDefaultAsync(
            e => e.Id == createEmployeeResult.Data.Id &&
                 e.LastName == createEmployeeResult.Data.LastName &&
                 e.JobId == ApplicationContextFactory.FirstJob.Id &&
                 e.DepartmentId == ApplicationContextFactory.FirstDepartment.Id,
            CancellationToken.None));
    }

    [Fact]
    public async Task FailOnNonExistingJob()
    {
        // Arrange
        var handler = new CreateEmployeeCommandHandler(Context);
        var nonExistingJobId = 5000;

        // Act
        var createEmployeeResult = await handler.Handle(new CreateEmployeeCommand
        {
            LastName = "Test",
            FirstName = "Test",
            MiddleName = "Test",
            DepartmentId = ApplicationContextFactory.FirstDepartment.Id,
            JobId = nonExistingJobId
        }, CancellationToken.None);

        // Assert
        Assert.False(createEmployeeResult.Succeed);
        Assert.Equal(createEmployeeResult.ErrorCode, ErrorCode.JobNotFound);
        Assert.Null(await Context.Employees.FirstOrDefaultAsync(e => e.JobId == nonExistingJobId, CancellationToken.None));
    }
    
    [Fact]
    public async Task FailOnNonExistingDepartment()
    {
        // Arrange
        var handler = new CreateEmployeeCommandHandler(Context);
        var nonExistingDepartmentId = 5000;

        // Act
        var createEmployeeResult = await handler.Handle(new CreateEmployeeCommand
        {
            LastName = "Test",
            FirstName = "Test",
            MiddleName = "Test",
            DepartmentId = nonExistingDepartmentId,
            JobId = ApplicationContextFactory.FirstJob.Id
        }, CancellationToken.None);

        // Assert
        Assert.False(createEmployeeResult.Succeed);
        Assert.Equal(createEmployeeResult.ErrorCode, ErrorCode.DepartmentNotFound);
        Assert.Null(await Context.Employees.FirstOrDefaultAsync(e => e.DepartmentId == nonExistingDepartmentId, CancellationToken.None));
    }
}