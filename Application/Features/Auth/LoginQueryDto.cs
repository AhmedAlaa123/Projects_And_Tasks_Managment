using MediatR;

namespace Application.Features.Auth;

public class LoginQueryDto:IRequest<LoginResponseDto>
{
    public LoginDto LoginDto { get; set; }
}
