

namespace Application.Features.Users.Commands.Models;
public class RegisterCommand :IRequest<string>
{
    public RegisterUserDto UserData { get; set; }
}
