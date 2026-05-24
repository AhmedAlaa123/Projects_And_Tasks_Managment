using Application.Features.Users.Commands.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Handlers;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;

    public Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
