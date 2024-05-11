using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Commands;
using AFCStudioEmployees.Application.CQRS.Queries;
using AFCStudioEmployees.Application.DTO;
using AFCStudioEmployees.Domain.Entities;
using AFCStudioEmployees.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AFCStudioEmployees.WebAPI.Controllers;

/// <summary>
/// Controller for employees
/// </summary>
[ApiController]
[Route("employees")]
[Produces("application/json")]
public class EmployeesController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator">Mediator instance from DI</param>
    public EmployeesController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Get all employees without pagination
    /// </summary>
    /// <returns>List of employees presentations</returns>
    /// <response code="200">Success</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Result<IEnumerable<EmployeePresentationDTO>>), StatusCodes.Status200OK)]
    public async Task<Result<IEnumerable<EmployeePresentationDTO>>> GetAllEmployees()
        => await _mediator.Send(new GetAllEmployeesPresentationsQuery());

    /// <summary>
    /// Get employees by filter, order and pagination
    /// </summary>
    /// <remarks>Error codes: invalid_pagination (page size less 1), property_name_not_found (non existing sort property), invalid_model (invalid request)</remarks>
    /// <param name="request">Request parameters with filter, order and pagination info</param>
    /// <returns>List of employees presentations in case of success</returns>
    /// <response code="200">Valid request and expected server response, possibly with error code ()</response>
    /// <response code="422">Request is not valid</response>
    [HttpGet]
    [ProducesResponseType(typeof(Result<IEnumerable<EmployeePresentationDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<EmployeePresentationDTO>>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<Result<IEnumerable<EmployeePresentationDTO>>> GetEmployees(
        [FromQuery] GetEmployeesRequest request)
    {
        var query = new GetEmployeesPresentationsQuery
        {
            PageSize = request.Size,
            PageIndex = request.Page - 1,
            SearchTerm = request.Search,
            SortProperty = request.SortBy
        };

        return await _mediator.Send(query);
    }

    /// <summary>
    /// Create new employee
    /// </summary>
    /// <remarks>Error codes: department_not_found, job_not_found (dep/job with given id not found)</remarks>
    /// <param name="request">Request with new employee data</param>
    /// <returns>Created employee in case of success</returns>
    /// <response code="200">Valid request and expected server response, possibly with error code</response>
    /// <response code="422">Request is not valid</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Employee>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<Result<Employee>> CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        var command = new CreateEmployeeCommand
        {
            LastName = request.LastName,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            BirthDate = request.BirthDate,
            DepartmentId = request.DepartmentId,
            JobId = request.JobId
        };

        return await _mediator.Send(command);
    }

    /// <summary>
    /// Update employee info
    /// </summary>
    /// <remarks>Error codes: employee_not_found, department_not_found, job_not_found (emp/dep/job with given id not found)</remarks>
    /// <param name="request">Request with updating employee data</param>
    /// <returns>Result with updated employee</returns>
    /// <response code="200">Valid request and expected server response, possibly with error code</response>
    /// <response code="422">Request is not valid</response>
    [HttpPut]
    [ProducesResponseType(typeof(Result<Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Employee>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<Result<Employee>> UpdateEmployee([FromBody] UpdateEmployeeRequest request)
    {
        var command = new UpdateEmployeeCommand
        {
            LastName = request.LastName,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            BirthDate = request.BirthDate,
            DepartmentId = request.DepartmentId,
            JobId = request.JobId,
            EmployeeId = request.EmployeeId
        };

        return await _mediator.Send(command);
    }

    /// <summary>
    /// Delete employee by id
    /// </summary>
    /// <remarks>Error codes: employee_not_found (employee with given id not found)</remarks>
    /// <param name="employeeId">Id of employee to delete</param>
    /// <returns>None in case of success</returns>
    /// <response code="200">Valid request and expected server response, possibly with error code</response>
    [HttpDelete("{employeeId:long}")]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status200OK)]
    public async Task<Result<None>> DeleteEmployee(long employeeId)
        => await _mediator.Send(new DeleteEmployeeCommand { EmployeeId = employeeId });

    /// <summary>
    /// Get employees pages count by employees per page
    /// </summary>
    /// <remarks>Error codes: invalid_pagination (employees per page less than 1)</remarks>
    /// <param name="employeesPerPage">Employees per page</param>
    /// <returns>Count of pages</returns>
    /// <response code="200">Valid request and expected server response, possibly with error code</response>
    [HttpGet("pages/{employeesPerPage:int}")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
    public async Task<Result<int>> GetEmployeesPagesCount(int employeesPerPage)
        => await _mediator.Send(new GetEmployeesPagesCountQuery { EmployeesPerPage = employeesPerPage });
}