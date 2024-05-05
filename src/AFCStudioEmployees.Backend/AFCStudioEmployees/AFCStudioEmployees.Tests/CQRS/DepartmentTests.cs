using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Commands;
using AFCStudioEmployees.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AFCStudioEmployees.Tests.CQRS;

/// <summary>
/// Unit tests for CQRS commands/queries of department entity
/// </summary>
public class DepartmentTests : BaseTest
{
    /// <summary>
    /// Successfull creating of unexisting department
    /// </summary>
    [Fact]
    public async Task CreateDepartmentHandler_Success()
    {
        // Arrange
        var handler = new CreateDepartmentCommandHandler(Context);
        var newDepartmentName = "FourthDepartment";

        // Act
        var createdDepartmentResult = await handler.Handle(new CreateDepartmentCommand
        {
            DepartmentName = newDepartmentName
        }, CancellationToken.None);

        // Assert
        Assert.True(createdDepartmentResult.Succeed);
        Assert.Null(createdDepartmentResult.ErrorCode);

        Assert.NotNull(await Context.Departments.FirstOrDefaultAsync(department =>
                department.Id == createdDepartmentResult.Data.Id && department.Name == newDepartmentName,
            CancellationToken.None));
    }
    
    [Fact]
    public async Task CreateDepartmentHandler_FailOnExistingName()
    {
        // Assert
        var handler = new CreateDepartmentCommandHandler(Context);

        // Act
        var createdDepartmentResult = await handler.Handle(new CreateDepartmentCommand
        {
            DepartmentName = ApplicationContextFactory.FirstDepartmentName
        }, CancellationToken.None);

        // Assert
        Assert.False(createdDepartmentResult.Succeed);
        Assert.Equal(createdDepartmentResult.ErrorCode, ErrorCode.DepartmentAlreadyExists);
    }

    [Fact]
    public async Task CreateDepartmentHandler_FailOnEmptyDepartmentName()
    {
        // Assert
        var handler = new CreateDepartmentCommandHandler(Context);
        
        // Act
        var createdDepartmentResult =
            await handler.Handle(new CreateDepartmentCommand { DepartmentName = "" }, CancellationToken.None);
        
        // Assert
        Assert.False(createdDepartmentResult.Succeed);
        Assert.Equal(createdDepartmentResult.ErrorCode, ErrorCode.DepartmentNameEmpty);
    }
}