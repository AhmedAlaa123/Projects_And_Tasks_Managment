using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Users.Commands.Models;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.Handlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManger;
    private readonly RoleManager<IdentityRole<int>> _roleManger;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManger, RoleManager<IdentityRole<int>> roleManger)
    {
        _userManger = userManger;
        _roleManger = roleManger;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserData.UserName,
            Email = request.UserData.EmailAddress,
            FirstName = request.UserData.FirstName,
            LastName = request.UserData.LastName,

        };
        var roles = _roleManger.Roles.Where(ele => request.UserData.Roles.Contains(ele.Id)).Select(ele=>ele.Name).ToList();
        var result=await  _userManger.CreateAsync(user);
        if (result.Succeeded)
        {
         var passResult=await   _userManger.AddPasswordAsync(user, request.UserData.PassWord);
            if (passResult.Succeeded) {
                // add user to roles
                var addRolesResult=await _userManger.AddToRolesAsync(user, roles);
            }
            else
            {
                throw new ValidationException(string.Join("\n", passResult.Errors.Select(ele => ele.Description).ToList()));
            }
                return "created";
        }
        else
        {
            throw new ValidationException(string.Join("\n", result.Errors.Select(ele => ele.Description).ToList())) ;
        }
    }
}
