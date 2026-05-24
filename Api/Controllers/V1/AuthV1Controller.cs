using Application.Features.Auth;

using Application.Features.Auth.Queries.Models;
using Application.Features.Roles.Queries;
using Asp.Versioning;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1;


[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/Auth")]
public class AuthV1Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthV1Controller(IMediator mediator)=> _mediator = mediator;
  
       
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _mediator.Send(new LoginQueryDto { LoginDto = loginDto });
     return result.IsLogined?  Ok(result):Unauthorized();     
    }
    
}
