using Api.Hubs;
using Application.Contracts;
using Application.Features.Projects.Commands.Models;
using Application.Features.Tasks.Commands.Models;
using Asp.Versioning;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/Task")]
[Authorize]
public class TaskV1Controller : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<NotificationHub> _hubContext;
 
    private readonly IUserService _userService;
    public TaskV1Controller(IMediator mediator, IHubContext<NotificationHub> hubContext, IUserService userService)
    {
        _mediator = mediator;
        _hubContext = hubContext;
        _userService = userService;
    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE}")]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto data)
    {

        var task = await _mediator.Send(new CreateTaskCommand { Task = data, UserId = _userService.GetUserID() });
        return Ok(task);

    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE}")]
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskUpdateDto data)
    {

        var task = await _mediator.Send(new UpdateTaskCommand { Task = data, });
        return Ok(task);

    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE},{Constants.USER_ROLE}")]
    [HttpPut("Update-Status")]
    public async Task<IActionResult> UpdateTaskStatus([FromBody] TaskUpdateStatusDto data)
    {

        var task = await _mediator.Send(new UpdateTaskStatusCommand { Status = data, });
        return Ok(task);

    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE}")]
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deletedTask = await _mediator.Send(new DeleteTaskCommand() { Id = id });
        return Ok(deletedTask);
    }
    
}
