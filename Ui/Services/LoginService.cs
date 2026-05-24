using Ui.Models.Auth;
using Ui.Services.Dtos;

namespace Ui.Services;

public class LoginService : ILoginService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public LoginService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginViewModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/Auth/login", model);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Login failed");
        }
        return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
    }
}
