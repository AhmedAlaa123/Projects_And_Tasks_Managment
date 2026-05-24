namespace Ui.Services.Dtos;

public class TaskUpdateStatusDto
{
    public int Id { get; set; }
    public Domain.Enums.TaskStatus Status { get; set; }
}
