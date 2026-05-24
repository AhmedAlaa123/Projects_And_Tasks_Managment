using Application.Responses;

namespace Application.Features.Tasks.Commands.Models;
public class DeleteTaskCommand : IRequest<BaseResponse<TaskInfoDto>>
{
    public int Id { get; set; }
}
