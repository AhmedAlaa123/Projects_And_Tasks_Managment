using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Ui.Services.Dtos;

public class CreateTaskDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public Domain.Enums.TaskStatus Status { get; set; }
    public Domain.Enums.TaskPriority Priority { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public int ProjectId { get; set; }
}
