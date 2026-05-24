using System.Security.Claims;
using Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Application.Services;
public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public int GetUserID()
    {
         
        var userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("UserId")!);
        return userId;
    }
}
