using Domain.Enums;

public class TaskListItemDto
{
    public int Id { get; set; }
    public string? Title { get; set; } 
    public string? Description { get; set; }
    public Domain.Enums.TaskStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedPerson { get; set; }
}
