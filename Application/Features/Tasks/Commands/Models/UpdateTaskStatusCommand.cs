using Application.Responses;

namespace Application.Features.Tasks.Commands.Models;
public class UpdateTaskStatusCommand:IRequest<BaseResponse<TaskInfoDto>>
{
    public TaskUpdateStatusDto Status { get; set; }
}
