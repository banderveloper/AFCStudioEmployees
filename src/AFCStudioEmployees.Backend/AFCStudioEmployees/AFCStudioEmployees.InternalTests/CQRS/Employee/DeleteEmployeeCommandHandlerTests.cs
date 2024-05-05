using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Commands;
using AFCStudioEmployees.InternalTests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AFCStudioEmployees.InternalTests.CQRS.Employee;

public class DeleteEmployeeCommandHandlerTests : BaseTest
{
    [Fact]
    public async Task Success()
    {
        // Arrange
        var handler = new DeleteEmployeeCommandHandler(Context);
        var employeeIdToDelete = ApplicationContextFactory.ThirdEmployee.Id;

        // Act
        var deleteEmployeeResult = await handler.Handle(new DeleteEmployeeCommand { EmployeeId = employeeIdToDelete },
            CancellationToken.None);

        // Assert
        Assert.True(deleteEmployeeResult.Succeed);
        Assert.Null(deleteEmployeeResult.ErrorCode);

        Assert.Null(await Context.Employees
            .FirstOrDefaultAsync(e => e.Id == employeeIdToDelete, CancellationToken.None));
    }

    [Fact]
    public async Task FailOnNonExistingId()
    {
        // Arrange
        var beforeDeleteEmployeesCount = await Context.Employees.CountAsync(CancellationToken.None);
        var handler = new DeleteEmployeeCommandHandler(Context);
        var nonExistingEmployeeId = 5000;

        // Act
        var deleteEmployeeResult = await handler.Handle(
            new DeleteEmployeeCommand { EmployeeId = nonExistingEmployeeId },
            CancellationToken.None);

        // Assert
        Assert.False(deleteEmployeeResult.Succeed);
        Assert.Equal(deleteEmployeeResult.ErrorCode, ErrorCode.EmployeeNotFound);
        Assert.Equal(await Context.Employees.CountAsync(CancellationToken.None), beforeDeleteEmployeesCount);
    }
}