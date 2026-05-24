using Application.Contracts;
using Application.Exceptions;
using Application.Features.Projects.Commands.Models;
using Application.Features.Projects.Queries.Models;
using Application.Features.Tasks.Queries.Models;
using Asp.Versioning;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/Projects")]
[Authorize]
public class ProjectV1Controller : ControllerBase
{
    #region Fields
    private readonly IMediator _mediator;
    private readonly IUserService _userService;
    #endregion
    #region Constructors    
    public ProjectV1Controller(IMediator mediator, IUserService userService)
    {
        _mediator = mediator;
        _userService = userService;
    }
    #endregion
    #region Endpoints
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE}")]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto data)
    {

        var project = await _mediator.Send(new CreateProjectCommand { Project = data, UserId = _userService.GetUserID() });
        return Ok(project);

    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE}")]
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateProject([FromBody] ProjectUpdateDto data)
    {

        var project = await _mediator.Send(new UpdateProjectCommand { Project = data, });
        return Ok(project);

    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE},{Constants.USER_ROLE}")]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllProjects(int PageNumber, int PageSize = 10)
    {
        var projectsPage =await _mediator.Send(new ProjecListQuery() { PageNumber = PageNumber, PageSize = PageSize });
        return Ok(projectsPage);
    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE},{Constants.USER_ROLE}")]
 
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetProjectDetail(int id)
    {
        var projectsPage = await _mediator.Send(new ProjectDetailQueryDto() {ProjectId=id});
        return Ok(projectsPage);
    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE}")]
 
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var deletedProject = await _mediator.Send(new DeleteProjectCommand() { Id = id });
        return Ok(deletedProject);
    }

    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE},{Constants.USER_ROLE}")]
    [HttpGet("get-tasks")]
    public async Task<IActionResult> GetProjectTasks(int id)
    {
        var projectTasks = await _mediator.Send(new TasksListQuery() { ProjectId = id });
        return Ok(projectTasks);
    }
    #endregion
}
