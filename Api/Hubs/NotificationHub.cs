
using MediatR;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Hubs;

public class NotificationHub : Hub
{
    private readonly IMediator _mediatory;

    public NotificationHub(IMediator mediatory) => _mediatory = mediatory;


    public override async Task OnConnectedAsync()
    {

        var connectionid = Context.ConnectionId;
        await base.OnConnectedAsync();
    }


}
