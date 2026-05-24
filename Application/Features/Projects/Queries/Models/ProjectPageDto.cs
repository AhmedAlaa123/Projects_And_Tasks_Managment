namespace Application.Features.Projects.Queries.Models;
public class ProjectPageDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<ProjectItemDto> Items { get; set; } = new();
}
