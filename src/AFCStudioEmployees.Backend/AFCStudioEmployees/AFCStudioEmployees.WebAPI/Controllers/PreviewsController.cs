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
public class PreviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PreviewsController(IMediator mediator)
        => _mediator = mediator;
    
    /// <summary>
    /// Get all departments (id+name)
    /// </summary>
    /// <returns>Result with list of departments dto</returns>
    [HttpGet("departments")]
    public async Task<Result<IEnumerable<DepartmentPreviewDTO>>> GetAllDepartmentsPreviews()
        => await _mediator.Send(new GetAllDepartmentsPreviewsQuery());

    /// <summary>
    /// Get all jobs (id+name)
    /// </summary>
    /// <returns>Result with list of jobs dto</returns>
    [HttpGet("jobs")]
    public async Task<Result<IEnumerable<JobPreviewDTO>>> GetAllJobsPreviews()
        => await _mediator.Send(new GetAllJobsPreviewsQuery());
}