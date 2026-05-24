using Application.Responses;

namespace Application.Features.Projects.Queries.Models;
public  class ProjectDetailQueryDto:IRequest<BaseResponse<ProjectDetailDto>>
{
    public int ProjectId { get; set; }
}
