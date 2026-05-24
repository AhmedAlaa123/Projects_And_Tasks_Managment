using Application.Features.Projects.Commands.Models;
using Application.Features.Projects.Queries.Models;
using Application.Responses;
using MediatR;

namespace Application.Features.Projects.Queries.Handlers;
public class ProjectDetailQueryHandler : IRequestHandler<ProjectDetailQueryDto, BaseResponse<ProjectDetailDto>>
{
    private readonly IReposatory<Project> _projectsReposatory;
    private readonly IMapper _mapper;
    public ProjectDetailQueryHandler(IReposatory<Project> projectsReposatory, IMapper mapper)
    {
        _projectsReposatory = projectsReposatory;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProjectDetailDto>> Handle(ProjectDetailQueryDto request, CancellationToken cancellationToken) {
        var projectModel = _projectsReposatory.GetById(request.ProjectId);
        return new() { Data = _mapper.Map<ProjectDetailDto>(projectModel),Success=true };
    }
}
