namespace Ui.Middlewares;

public class JwtCookieAuthMiddleware
{
    private readonly RequestDelegate _next;

    public JwtCookieAuthMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? "";
        var token = context.Request.Cookies["jwt_token"];

        var isAllowed = path.StartsWith("/Auth", StringComparison.OrdinalIgnoreCase) ||
                        path.StartsWith("/lib", StringComparison.OrdinalIgnoreCase) ||
                        path.StartsWith("/css", StringComparison.OrdinalIgnoreCase) ||
                        path.StartsWith("/js", StringComparison.OrdinalIgnoreCase) ||
                        path.StartsWith("/img", StringComparison.OrdinalIgnoreCase) ||
                        path.StartsWith("/favicon", StringComparison.OrdinalIgnoreCase) ||
                        path == "/";

        if (!isAllowed && string.IsNullOrEmpty(token))
        {
            context.Response.Redirect("/Auth/Login");
            return;
        }

        await _next(context);
    }
}
