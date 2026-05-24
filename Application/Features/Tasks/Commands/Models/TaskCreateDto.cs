using Domain.Enums;

namespace Application.Features.Tasks.Commands.Models;
public class TaskCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Domain.Enums.TaskStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
     
}
