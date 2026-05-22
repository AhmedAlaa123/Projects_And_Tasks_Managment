using Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Command
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRolevm, int>
    {
        private readonly     IReposatory<IdentityRole> _reposatory;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public CreateRoleCommandHandler(IReposatory<IdentityRole> reposatory, RoleManager<IdentityRole<int>> roleManager)
        {
            _reposatory = reposatory;
            _roleManager = roleManager;
        }

        public async Task<int> Handle(CreateRolevm request, CancellationToken cancellationToken)
        {
            var role=new IdentityRole<int> { Name= request.RoleName};
         var result=   await _roleManager.CreateAsync(role);
            
            return role.Id;
        }
    }
}
