using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Application.Features.Projects.Commands.Models;
public class CreateProjectCommand : IRequest<BaseResponse<ProjectInfoDto>>
{
    public ProjectCreateDto Project { get; set; }
    public int UserId { get; set; }

}
