
using Application.Features.Projects.Commands.Models;
using Application.Responses;
namespace Application.Features.Projects.Commands.Handlers;
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, BaseResponse<ProjectInfoDto>>
{
    private readonly IReposatory<Project> _projectsReposatory;
    private readonly IMapper _mapper;
    public CreateProjectCommandHandler(IReposatory<Project> projectsReposatory, IMapper mapper)
    {
        _projectsReposatory = projectsReposatory;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProjectInfoDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var projectModel=_mapper.Map<Project>(request.Project);
        projectModel.CreatedBy = request.UserId;
       await _projectsReposatory.Add(projectModel);
        return  new()
        {
            Data = _mapper.Map<ProjectInfoDto>(projectModel),
            Success=true,
            Message="Project Created"
        };
    }
}
