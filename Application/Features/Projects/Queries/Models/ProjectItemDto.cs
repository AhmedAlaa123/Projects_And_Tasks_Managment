using Application.Features.Projects.Commands.Models;

namespace Application.Features.Projects.Queries.Models;
public class ProjectItemDto:ProjectInfoDto
{
    public string? CreatedPreson { get; set; }
}
