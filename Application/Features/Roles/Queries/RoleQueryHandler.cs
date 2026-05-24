using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Queries;

public class RoleQueryHandler : IRequestHandler<RoleQueryDto, RoleDto>, IRequestHandler<RoleListQueryDto, List<RoleDto>>
{
    private readonly RoleManager<IdentityRole<int>> _roleManger;

    public RoleQueryHandler(RoleManager<IdentityRole<int>> roleManger)=> _roleManger = roleManger;


    public async Task<List<RoleDto>> Handle(RoleListQueryDto request, CancellationToken cancellationToken)
    {
       var roles=await _roleManger.Roles.Select(ele=>new RoleDto { Id=ele.Id,RoleName=ele.Name}).ToListAsync();
        return roles;
    }

    public async Task<RoleDto> Handle(RoleQueryDto request, CancellationToken cancellationToken)
    {
        var roles = await _roleManger.Roles.FirstOrDefaultAsync(ele=>ele.Name==request.RoleName);
        if (roles == null)
        {
            return null;
        }
        return new RoleDto
        {
            Id = roles.Id,
            RoleName = roles.Name
        };
    }
}
