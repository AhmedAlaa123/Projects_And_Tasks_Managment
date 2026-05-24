using Application.Features.Tasks.Commands.Models;
using Application.Responses;

namespace Application.Features.Tasks.Commands.Handlers;
public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, BaseResponse<TaskInfoDto>>, IRequestHandler<UpdateTaskStatusCommand, BaseResponse<TaskInfoDto>>
{
    private readonly IReposatory<Domain.Models.Task> _tasksReposatory;
    private readonly IReposatory<Domain.Models.Project> _projectsReposatory;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(IMapper mapper, IReposatory<Project> projectsReposatory, IReposatory<Domain.Models.Task> tasksReposatory)
    {
        _mapper = mapper;
        _projectsReposatory = projectsReposatory;
        _tasksReposatory = tasksReposatory;
    }

    public async Task<BaseResponse<TaskInfoDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        // get task 
        var task = _tasksReposatory.GetById(request.Task.Id);
        // get project
        var project = _projectsReposatory.GetById(request.Task.ProjectId);
        // udpate task
        task = _mapper.Map(request.Task, task);
        await _tasksReposatory.Update(task);
        return new() { Data = _mapper.Map<TaskInfoDto>(task), Success = true };
    }

    public async Task<BaseResponse<TaskInfoDto>> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = _tasksReposatory.GetById(request.Status.Id);
        task.Status = request.Status.Status;
        await _tasksReposatory.Update(task);
        return new() { Data = _mapper.Map<TaskInfoDto>(task), Success = true };
    }
}
