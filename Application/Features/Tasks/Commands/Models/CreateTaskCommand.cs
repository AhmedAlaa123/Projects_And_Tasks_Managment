using Application.Responses;

namespace Application.Features.Tasks.Commands.Models;
public class CreateTaskCommand:IRequest<BaseResponse<TaskInfoDto>>
{
    public TaskCreateDto Task { get; set; }
    public int? UserId { get; set; }
}
