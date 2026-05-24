using Application.Features.Auth.Queries.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Queries;

public class AuthQueryHandler : IRequestHandler<LoginQueryDto, LoginResponseDto>
{
    private readonly AuthService _authService;
    private readonly UserManager<ApplicationUser> _userManager;
 

    public AuthQueryHandler(AuthService authService, UserManager<ApplicationUser> userManager)
    {
        _authService = authService;
        _userManager = userManager;
    }

    public async Task<LoginResponseDto> Handle(LoginQueryDto request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.LoginDto.UserName);
        if (user == null)
        {
            return new LoginResponseDto { IsLogined = false, Token = null };

        }

        // Check password
        var hassPassword = await _userManager.CheckPasswordAsync(user, request.LoginDto.Password);
        if (!hassPassword)
        {

            return new LoginResponseDto { IsLogined = false, Token = null };
        }
       var role= _userManager.GetRolesAsync(user).Result.FirstOrDefault();
        // Generate JWT
        var token = _authService.GenerateToken(user.UserName,role,user.Id);

        return new LoginResponseDto { IsLogined = true, Token = token };

    }
}
