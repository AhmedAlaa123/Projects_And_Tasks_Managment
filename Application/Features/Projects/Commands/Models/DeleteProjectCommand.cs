using Application.Responses;

namespace Application.Features.Projects.Commands.Models;
public class DeleteProjectCommand : IRequest<BaseResponse<ProjectInfoDto>>
{
    public int Id { get; set; }
}
