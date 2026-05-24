namespace Ui.Services.Dtos;

public class ProjectDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public string? CreatedPreson { get; set; }
}
