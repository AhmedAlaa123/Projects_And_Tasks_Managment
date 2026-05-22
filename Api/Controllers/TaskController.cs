using Api.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<NotificationHub> _hubContext;

    public TaskController(IMediator mediator, IHubContext<NotificationHub> hubContext)
    {
        _mediator = mediator;
        _hubContext = hubContext;
    }

    //[Authorize(Roles ="admin")]
    //[HttpPost("CreateNotification")]
    //public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto notificationDto)
    //{
    //    var notification = await _mediator.Send(notificationDto);
    //    await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
    //    return Ok(notification);

    //}
}
