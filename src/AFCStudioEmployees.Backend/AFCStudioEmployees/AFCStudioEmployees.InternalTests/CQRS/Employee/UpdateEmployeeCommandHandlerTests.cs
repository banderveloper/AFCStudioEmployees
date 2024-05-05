using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Commands;
using AFCStudioEmployees.InternalTests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AFCStudioEmployees.InternalTests.CQRS.Employee;

public class UpdateEmployeeCommandHandlerTests : BaseTest
{
    [Fact]
    public async Task Success()
    {
        // Arrange
        var employeeIdToUpdate = ApplicationContextFactory.SecondEmployee.Id;

        var newLastName = "Updated";
        var newJobId = ApplicationContextFactory.FirstJob.Id;

        var handler = new UpdateEmployeeCommandHandler(Context);

        // Act
        var updateEmployeeResult = await handler.Handle(new UpdateEmployeeCommand
        {
            EmployeeId = employeeIdToUpdate,
            LastName = newLastName,
            JobId = newJobId,

            FirstName = ApplicationContextFactory.SecondEmployee.FirstName,
            MiddleName = ApplicationContextFactory.SecondEmployee.MiddleName,
            DepartmentId = ApplicationContextFactory.SecondEmployee.DepartmentId,
            BirthDate = ApplicationContextFactory.SecondEmployee.Birthdate
        }, CancellationToken.None);

        // Assert
        Assert.True(updateEmployeeResult.Succeed);
        Assert.Null(updateEmployeeResult.ErrorCode);

        Assert.NotNull(await Context.Employees.FirstOrDefaultAsync(
            e => e.Id == employeeIdToUpdate && 
                 e.LastName == newLastName && 
                 e.JobId == newJobId,
            CancellationToken.None));
    }

    [Fact]
    public async Task FailOnNonExistingDepartmentId()
    {
        var employeeIdToUpdate = ApplicationContextFactory.SecondEmployee.Id;

        var nonExistingDepartmentId = 5000;

        var handler = new UpdateEmployeeCommandHandler(Context);

        // Act
        var updateEmployeeResult = await handler.Handle(new UpdateEmployeeCommand
        {
            EmployeeId = employeeIdToUpdate,
            DepartmentId = nonExistingDepartmentId,
            
            FirstName = ApplicationContextFactory.SecondEmployee.FirstName,
            MiddleName = ApplicationContextFactory.SecondEmployee.MiddleName,
            BirthDate = ApplicationContextFactory.SecondEmployee.Birthdate,
            LastName = ApplicationContextFactory.SecondEmployee.LastName,
            JobId = ApplicationContextFactory.SecondEmployee.JobId
        }, CancellationToken.None);

        // Assert
        Assert.False(updateEmployeeResult.Succeed);
        Assert.Equal(updateEmployeeResult.ErrorCode, ErrorCode.DepartmentNotFound);
    }
    
    [Fact]
    public async Task FailOnNonExistingJobId()
    {
        var employeeIdToUpdate = ApplicationContextFactory.SecondEmployee.Id;

        var nonExistingJobId = 5000;

        var handler = new UpdateEmployeeCommandHandler(Context);

        // Act
        var updateEmployeeResult = await handler.Handle(new UpdateEmployeeCommand
        {
            EmployeeId = employeeIdToUpdate,
            JobId = nonExistingJobId,
            
            DepartmentId = ApplicationContextFactory.SecondEmployee.JobId,
            FirstName = ApplicationContextFactory.SecondEmployee.FirstName,
            MiddleName = ApplicationContextFactory.SecondEmployee.MiddleName,
            BirthDate = ApplicationContextFactory.SecondEmployee.Birthdate,
            LastName = ApplicationContextFactory.SecondEmployee.LastName,
        }, CancellationToken.None);

        // Assert
        Assert.False(updateEmployeeResult.Succeed);
        Assert.Equal(updateEmployeeResult.ErrorCode, ErrorCode.JobNotFound);
    }
}