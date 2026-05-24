using Application.Features.Projects.Queries.Models;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Projects.Queries.Handlers;
public class ProjectsListQueryHandler : IRequestHandler<ProjecListQuery, BaseResponse<ProjectPageDto>>
{
    private readonly IReposatory<Project> _projectsReposatory;
 

    public ProjectsListQueryHandler(IReposatory<Project> projectsReposatory) => _projectsReposatory = projectsReposatory;


    public async Task<BaseResponse<ProjectPageDto>> Handle(ProjecListQuery request, CancellationToken cancellationToken)
    {
        var totalCount =await _projectsReposatory.TotalCount();
        var projects =await _projectsReposatory.GetAll().Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).Include(ele => ele.CreatorUser).AsNoTracking().Select(ele => new ProjectItemDto
        {
            CreatedAt = ele.CreatedAt,
            CreatedPreson= ele.CreatorUser==null?string.Empty: ele.CreatorUser.FirstName+" "+ele.CreatorUser.LastName,
            CreatedBy=ele.CreatedBy,
            Description=ele.Description,
            Id=ele.Id,
            Name = ele.Name
        }).ToListAsync();
        return new()
        {
            Data=new ProjectPageDto()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Items = projects,
                TotalCount = totalCount
            },
            Success=true
        };
    }
}
