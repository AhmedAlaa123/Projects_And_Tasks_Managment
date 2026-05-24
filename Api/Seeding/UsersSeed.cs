using Application.Features.Roles.Command;
using Application.Features.Roles.Queries;
using Application.Features.Users.Commands;
using Application.Features.Users.Commands.Models;
using Domain.Models;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Seeding;

public static class UsersSeed
{
    public static async System.Threading.Tasks.Task SeedAsync(UserManager<ApplicationUser> _userManager, IMediator _mediator)
    {
        // seed roles
        if ((await _mediator.Send(new RoleQueryDto() { RoleName = Constants.ADMIN_ROLE })) is null)
        {
            await _mediator.Send(new CreateRolevm { RoleName = Constants.ADMIN_ROLE });
        }
        if ((await _mediator.Send(new RoleQueryDto() { RoleName = Constants.USER_ROLE })) is null)
        {
            await _mediator.Send(new CreateRolevm { RoleName = Constants.USER_ROLE });
        }
        if ((await _mediator.Send(new RoleQueryDto() { RoleName = Constants.MANAGER_ROLE })) is null)
        {
            await _mediator.Send(new CreateRolevm { RoleName = Constants.MANAGER_ROLE });
        }
        var usersCount = await _userManager.Users.Where(i => i.UserName == Constants.ADMIN_ROLE).CountAsync();


        if (usersCount <= 0)
        {
            var roledata = await _mediator.Send(new RoleQueryDto() { RoleName = Constants.ADMIN_ROLE });
            await _mediator.Send(new RegisterCommand
            {
                UserData = new RegisterUserDto()
                {
                    UserName = "admin",
                    EmailAddress = "admin@project.com",
                    FirstName = "Ahmed",
                    LastName = "Alaa",
                    PassWord = "@Aa123456",
                    Roles = new List<int>() { roledata.Id }
                }
            });
        }
    }
}

