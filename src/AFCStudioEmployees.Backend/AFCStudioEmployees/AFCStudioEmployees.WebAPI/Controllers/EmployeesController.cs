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
public class EmployeesController
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Get all employees without pagination
    /// </summary>
    /// <returns>Result with list of employees</returns>
    [HttpGet("all")]
    public async Task<Result<IEnumerable<EmployeePresentationDTO>>> GetAllEmployees()
        => await _mediator.Send(new GetAllEmployeesPresentationsQuery());

    /// <summary>
    /// Get employees by filter, order and pagination
    /// </summary>
    /// <param name="request">Request parameters with filter, order and pagination info</param>
    /// <returns>Result with list of employees</returns>
    [HttpGet]
    public async Task<Result<IEnumerable<EmployeePresentationDTO>>> GetEmployees([FromQuery] GetEmployeesRequest request)
    {
        var query = new GetEmployeesPresentationsQuery
        {
            PageSize = request.Size,
            PageIndex = request.Page,
            SearchTerm = request.Search,
            SortProperty = request.SortBy
        };

        return await _mediator.Send(query);
    }

    /// <summary>
    /// Create new employee
    /// </summary>
    /// <param name="request">Request with new employee data</param>
    /// <returns>Result with created employee</returns>
    [HttpPost]
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
}