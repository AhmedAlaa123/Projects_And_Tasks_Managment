using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ui.Extensions;
using Ui.Handlers;
using Ui.Middlewares;
using Ui.Services;

namespace Ui;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSession();

        builder.Services.AddTransient<JwtCookieHandler>();
        var baseUrl = builder.Configuration["ApiSettings:BaseUrl"]!;

        builder.Services.AddHttpClients(builder.Configuration);
        
      
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/Login";
                    options.LogoutPath = "/auth/Logout";
                    options.AccessDeniedPath = "/auth/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.Name = "jwt_token";
                });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }


        app.UseHttpsRedirection();
        app.UseStaticFiles();                           
         
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();                             
        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=auth}/{action=login}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
