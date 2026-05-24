using Application.Responses;

namespace Application.Features.Tasks.Commands.Models;
public class UpdateTaskCommand:IRequest<BaseResponse<TaskInfoDto>>
{
    public TaskUpdateDto Task { get; set; }
}
