using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserVm, string>
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole<int>> _roleManger;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManger, RoleManager<IdentityRole<int>> roleManger)
        {
            _userManger = userManger;
            _roleManger = roleManger;
        }

        public async Task<string> Handle(CreateUserVm request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,

            };
            var roles = _roleManger.Roles.Where(ele => request.RoleId.Contains(ele.Id)).Select(ele=>ele.Name).ToList();
          var result=await  _userManger.CreateAsync(user);
            if (result.Succeeded)
            {
             var passResult=await   _userManger.AddPasswordAsync(user, request.Password);
                if (passResult.Succeeded) {
                    // add user to roles
                    var addRolesResult=await _userManger.AddToRolesAsync(user, roles);
                }
                return "created";
            }
            else
            {
                return "Not Created";
            }
        }
    }
}
