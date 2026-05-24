using MediatR;

namespace Application.Features.Auth.Queries.Models;

public class LoginQueryDto:IRequest<LoginResponseDto>
{
    public LoginDto LoginDto { get; set; }
}
