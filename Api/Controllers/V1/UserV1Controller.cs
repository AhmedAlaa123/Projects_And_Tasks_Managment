using Application.Features.Roles.Queries;
using Application.Features.Users.Commands.Models;
using Asp.Versioning;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/Auth")]
public class UserV1Controller : ControllerBase
{
    private readonly IMediator _mediator;
    public UserV1Controller(IMediator mediator) => _mediator = mediator;

    [Authorize(Roles = $"{Constants.ADMIN_ROLE},{Constants.MANAGER_ROLE}")]
    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _mediator.Send(new RoleListQueryDto());
        return Ok(roles);
    }
    [Authorize(Roles = $"{Constants.ADMIN_ROLE}")]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterUserDto userData)
    {
        var roles = await _mediator.Send(new RegisterCommand() { UserData=userData});
        return Ok(roles);
    }

}
