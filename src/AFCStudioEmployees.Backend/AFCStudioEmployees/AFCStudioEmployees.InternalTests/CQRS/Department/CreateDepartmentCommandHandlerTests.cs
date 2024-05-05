using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Commands;
using AFCStudioEmployees.InternalTests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AFCStudioEmployees.InternalTests.CQRS.Department;

/// <summary>
/// Unit tests for create department command handler
/// </summary>
public class CreateDepartmentCommandHandlerTests : BaseTest
{
    /// <summary>
    /// Successfull creating of unexisting department
    /// </summary>
    [Fact]
    public async Task Success()
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
    
    /// <summary>
    /// Failing creating of department with existing name
    /// </summary>
    [Fact]
    public async Task FailOnExistingName()
    {
        // Arrange
        var handler = new CreateDepartmentCommandHandler(Context);

        // Act
        var createdDepartmentResult = await handler.Handle(new CreateDepartmentCommand
        {
            DepartmentName = ApplicationContextFactory.FirstDepartment.Name
        }, CancellationToken.None);

        // Assert
        Assert.False(createdDepartmentResult.Succeed);
        Assert.Equal(createdDepartmentResult.ErrorCode, ErrorCode.DepartmentAlreadyExists);
    }
    
    /// <summary>
    /// Failing creating of department with existing name, got in the uppercase
    /// </summary>
    [Fact]
    public async Task FailOnExistingNameInUpperCase()
    {
        // Arrange
        var handler = new CreateDepartmentCommandHandler(Context);

        // Act
        var createdDepartmentResult = await handler.Handle(new CreateDepartmentCommand
        {
            DepartmentName = ApplicationContextFactory.FirstDepartment.Name.ToUpper()
        }, CancellationToken.None);

        // Assert
        Assert.False(createdDepartmentResult.Succeed);
        Assert.Equal(createdDepartmentResult.ErrorCode, ErrorCode.DepartmentAlreadyExists);
    }

    /// <summary>
    /// Failing creating of department with empty name
    /// </summary>
    [Fact]
    public async Task FailOnEmptyDepartmentName()
    {
        // Arrange
        var handler = new CreateDepartmentCommandHandler(Context);
        
        // Act
        var createdDepartmentResult =
            await handler.Handle(new CreateDepartmentCommand { DepartmentName = "" }, CancellationToken.None);
        
        // Assert
        Assert.False(createdDepartmentResult.Succeed);
        Assert.Equal(createdDepartmentResult.ErrorCode, ErrorCode.DepartmentNameEmpty);
    }
}