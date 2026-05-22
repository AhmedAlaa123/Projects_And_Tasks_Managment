using Application.Features.Roles.Command;
using Application.Features.Roles.Queries;
using Application.Features.Users.Commands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Seeding;
 
    public static class UsersSeed
    {
        public static async System.Threading.Tasks.Task SeedAsync(UserManager<ApplicationUser> _userManager, IMediator _mediator)
        {
            // seed roles
            if ((await _mediator.Send(new RoleQueryDto() { RoleName = "admin" })) is null )
            {
                await _mediator.Send(new CreateRolevm { RoleName = "admin" });
            }
            if ((await _mediator.Send(new RoleQueryDto() { RoleName = "user" })) is null)
            {
                await _mediator.Send(new CreateRolevm { RoleName = "user" });
            }
            if ((await _mediator.Send(new RoleQueryDto() { RoleName = "manger" })) is null)
            {
                await _mediator.Send(new CreateRolevm { RoleName = "manger" });
            }
            var usersCount = await _userManager.Users.Where(i => i.UserName == "admin").CountAsync();
            

            if (usersCount <= 0)
            {
                var roledata = await _mediator.Send(new RoleQueryDto() { RoleName = "admin" });
                 
               
                
                await _mediator.Send(new CreateUserVm()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FirstName = "Ahmed",
                    LastName = "Alaa",
                    Password = "@Aa123456",

                    RoleId = new List<int>() { roledata.Id }
                });
            }
        }
    }

