using AFCStudioEmployees.Application;
using AFCStudioEmployees.Application.CQRS.Queries;
using AFCStudioEmployees.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AFCStudioEmployees.WebAPI.Controllers;

/// <summary>
/// Controller for getting list of departments and jobs (id+name, for frontend select options) 
/// </summary>
[ApiController]
[Route("previews")]
[Produces("application/json")]
public class PreviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator">Mediator instance from DI</param>
    public PreviewsController(IMediator mediator)
        => _mediator = mediator;
    
    /// <summary>
    /// Get all departments
    /// </summary>
    /// <returns>List of departments previews</returns>
    /// <response code="200">Success</response>
    [HttpGet("departments")]
    [ProducesResponseType(typeof(Result<IEnumerable<DepartmentPreviewDTO>>), StatusCodes.Status200OK)]
    public async Task<Result<IEnumerable<DepartmentPreviewDTO>>> GetAllDepartmentsPreviews()
        => await _mediator.Send(new GetAllDepartmentsPreviewsQuery());

    /// <summary>
    /// Get all jobs
    /// </summary>
    /// <returns>List of jobs previews</returns>
    /// <response code="200">Success</response>
    [HttpGet("jobs")]
    [ProducesResponseType(typeof(Result<IEnumerable<JobPreviewDTO>>), StatusCodes.Status200OK)]
    public async Task<Result<IEnumerable<JobPreviewDTO>>> GetAllJobsPreviews()
        => await _mediator.Send(new GetAllJobsPreviewsQuery());
}