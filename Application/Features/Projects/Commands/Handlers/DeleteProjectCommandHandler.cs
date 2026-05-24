using Application.Exceptions;
using Application.Features.Projects.Commands.Models;
using Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Projects.Commands.Handlers;
public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, BaseResponse<ProjectInfoDto>>
{
    private readonly IReposatory<Project> _projectsReposatory;
    private readonly IReposatory<Domain.Models.Task> _tasksReposatory;
    private readonly IMapper _mapper;
    public DeleteProjectCommandHandler(IReposatory<Project> projectsReposatory, IMapper mapper, IReposatory<Domain.Models.Task> tasksReposatory)
    {
        _projectsReposatory = projectsReposatory;
        _mapper = mapper;
        _tasksReposatory = tasksReposatory;
    }
    public async Task<BaseResponse<ProjectInfoDto>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var projectModel = _projectsReposatory.GetById(request.Id);
        // check if project has Tasks
        if (await _tasksReposatory.GetAll().AnyAsync(ele => ele.ProjectId == request.Id))
        {
            throw new ValidationException("Can't Delete Project Because Has Assigned Tasks");
        }
        await _projectsReposatory.Delete(projectModel);
        return new()
        {
            Data = _mapper.Map<ProjectInfoDto>(projectModel),
            Message = "Project Created Successfuly",
            Success = true
        };
    }
}
