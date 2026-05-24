using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ui.Models.Auth;
using Ui.Services;

namespace Ui.Controllers;
public class AuthController : Controller
{
    private readonly ILoginService _loginService;

    public AuthController(ILoginService loginService) => _loginService = loginService;

    [HttpGet]
    public IActionResult Login() => View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);

        }

        try
        {
            var result = await _loginService.LoginAsync(model);

            // Store token in cookie
            Response.Cookies.Append("jwt_token", result!.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
            // sign in user
            var claims = new List<Claim>
            {
                
                new Claim("jwt_token",               result.Token)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              principal,
              new AuthenticationProperties
              {
                  IsPersistent = model.RememberMe,
                  ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
              });

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt_token");
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
