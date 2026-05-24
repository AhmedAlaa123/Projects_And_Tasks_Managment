using Application.Responses;
using MediatR;
namespace Application.Features.Projects.Commands.Models;
public class UpdateProjectCommand : IRequest<BaseResponse<ProjectInfoDto>>
{
    public ProjectUpdateDto Project { get; set; }
  

}
