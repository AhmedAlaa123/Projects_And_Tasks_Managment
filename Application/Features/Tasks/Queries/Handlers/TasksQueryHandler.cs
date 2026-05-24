using Application.Features.Tasks.Queries.Models;
using Application.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tasks.Queries.Handlers;
public class TasksQueryHandler : IRequestHandler<TasksListQuery, BaseResponse<List<TaskListItemDto>>>
{
    private readonly IReposatory<Domain.Models.Task> _taskReposatory;

    public TasksQueryHandler(IReposatory<Domain.Models.Task> taskReposatory) => _taskReposatory = taskReposatory;

    public async Task<BaseResponse<List<TaskListItemDto>>> Handle(TasksListQuery request, CancellationToken cancellationToken)
    {
        var taskes = await _taskReposatory.GetAll().Where(ele => ele.ProjectId == request.ProjectId).AsNoTracking().Select(ele => new TaskListItemDto
        {
            Id = ele.Id,
            CreatedBy = ele.CreatedBy,
            CreatedPerson = ele.CreatorUser != null ? $"{ele.CreatorUser.FirstName} {ele.CreatorUser.LastName}" : "_",
            Description = ele.Description,
            DueDate = ele.DueDate,
            Priority = ele.Priority,
            ProjectId = ele.ProjectId,
            Status = ele.Status,
            Title = ele.Title

        }).ToListAsync();
        return new() { Data = taskes, Success = true };
    }
}
