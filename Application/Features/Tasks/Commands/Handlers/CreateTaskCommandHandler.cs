using Application.Features.Tasks.Commands.Models;
using Application.Responses;

namespace Application.Features.Tasks.Commands.Handlers;
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, BaseResponse<TaskInfoDto>>
{
    private readonly IReposatory<Domain.Models.Task> _tasksReposatory;
    private readonly IReposatory<Domain.Models.Project> _projectsReposatory;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(IReposatory<Domain.Models.Task> tasksReposatory, IMapper mapper, IReposatory<Project> projectsReposatory)
    {
        _tasksReposatory = tasksReposatory;
        _mapper = mapper;
        _projectsReposatory = projectsReposatory;
    }

    public async Task<BaseResponse<TaskInfoDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // check project is found
        var project = _projectsReposatory.GetById(request.Task.ProjectId);
        var taskModel= _mapper.Map<Domain.Models.Task>(request.Task);
        taskModel.CreatedBy = request.UserId;
        await _tasksReposatory.Add(taskModel);
        return new() { Data = _mapper.Map<TaskInfoDto>(taskModel) ,Success=true}; 
    }
}
