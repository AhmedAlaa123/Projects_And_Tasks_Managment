using System.Net.Http.Headers;
using System.Security.Claims;

namespace Ui.Handlers;

public class JwtCookieHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtCookieHandler(IHttpContextAccessor httpContextAccessor)=> _httpContextAccessor = httpContextAccessor;
   
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext?.User
           .FindFirstValue("jwt_token");

        if (string.IsNullOrEmpty(token))
        {
            
            token = _httpContextAccessor.HttpContext?.Request.Cookies["jwt_token"];
        }

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
