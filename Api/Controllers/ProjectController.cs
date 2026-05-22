using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    #region Fields
    private readonly IMediator _mediator;
    #endregion
    #region Constructors
    public ProjectController(IMediator mediator) => _mediator = mediator;
    #endregion
    #region Endpoints
    //[Authorize(Roles = "admin")]
    //[HttpPost("CreateClient")]
    //public async Task<IActionResult> CreateClient([FromBody] CreateClientDto createClientDto)
    //{
    //    var client = await _mediator.Send(createClientDto);
    //    return Ok(client);
    //}
    //[Authorize(Roles = "admin,manger")]
    //[HttpGet("GetAllClients")]
    //public async Task<IActionResult> GetAllClients()
    //{
    //    var clients = await _mediator.Send(new ClientQueryDto());
    //    return Ok(clients);

    //}
    #endregion
}
