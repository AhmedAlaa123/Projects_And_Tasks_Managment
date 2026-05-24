using Ui.Models.Auth;
using Ui.Services.Dtos;

namespace Ui.Services;

public interface ILoginService
{
    Task<LoginResponseDto?> LoginAsync(LoginViewModel model);
}
