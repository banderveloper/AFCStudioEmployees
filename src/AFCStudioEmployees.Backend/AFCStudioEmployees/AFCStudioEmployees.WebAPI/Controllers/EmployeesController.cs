using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Queries;
using AFCStudioEmployees.Application.DTO;
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
    [HttpGet]
    public async Task<Result<IEnumerable<EmployeePresentationDTO>>> GetAllEmployees()
        => await _mediator.Send(new GetAllEmployeesPresentationsQuery());
}