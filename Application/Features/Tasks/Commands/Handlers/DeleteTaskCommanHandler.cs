using Application.Features.Tasks.Commands.Models;
using Application.Responses;

namespace Application.Features.Tasks.Commands.Handlers;
public class DeleteTaskCommanHandler : IRequestHandler<DeleteTaskCommand, BaseResponse<TaskInfoDto>>
{
    private readonly IReposatory<Domain.Models.Task> _tasksReposatory;
    private readonly IMapper _mapper;

    public DeleteTaskCommanHandler(IMapper mapper, IReposatory<Domain.Models.Task> tasksReposatory)
    {
        _mapper = mapper;
        _tasksReposatory = tasksReposatory;
    }

    public async Task<BaseResponse<TaskInfoDto>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = _tasksReposatory.GetById(request.Id);
        await _tasksReposatory.Delete(task);
        return new()
        {
            Data = _mapper.Map<TaskInfoDto>(task),
            Success = true
        };  
    }
}
