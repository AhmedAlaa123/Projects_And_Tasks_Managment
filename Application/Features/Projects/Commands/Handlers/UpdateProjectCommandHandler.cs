using Application.Features.Projects.Commands.Models;
using Application.Responses;

namespace Application.Features.Projects.Commands.Handlers;
public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, BaseResponse<ProjectInfoDto>>
{
    private readonly IReposatory<Project> _projectsReposatory;
    private readonly IMapper _mapper;
    public UpdateProjectCommandHandler(IReposatory<Project> projectsReposatory, IMapper mapper)
    {
        _projectsReposatory = projectsReposatory;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProjectInfoDto>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        // find project
        var projectModel = _projectsReposatory.GetById(request.Project.Id);
        _mapper.Map(request.Project, projectModel);
        await _projectsReposatory.Update(projectModel);
  
        return new(){
          Data=_mapper.Map<ProjectInfoDto>(projectModel),
          Success=true,
          Message="Project Created Successfuly"
        };
    }
}
