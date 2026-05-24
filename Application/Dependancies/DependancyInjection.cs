using Application.Contracts;
using Application.Features.Projects.Commands.Validations;
using Application.Profiles;
using Application.Reposatories;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using InfraStructure.context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
namespace Application.Dependancies;
public static class DependancyInjection
{
    public static void AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        // auto mapper registeration
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ApplicationMapper>();
        });
         
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly));
        // register fluent validation
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateProjectValidator>();

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var config = ConfigurationOptions.Parse(
                 configuration["Redis:ConnectionString"]!);

            config.AbortOnConnectFail = false;
            config.ConnectRetry = 3;
            config.ConnectTimeout = 5000;

            return ConnectionMultiplexer.Connect(config);
        });


    }
    public static void AddScopedService(this IServiceCollection services) {

        services.AddScoped<IUserService,UserService>();
        services.AddScoped(typeof(IReposatory<>), typeof(Reposatory<>));
        services.AddScoped<AuthService>();
        services.AddIdentity<ApplicationUser, IdentityRole<int>>(option =>
        {

            // Password settings.
            option.Password.RequireDigit = true;
            option.Password.RequireLowercase = true;
            option.Password.RequireNonAlphanumeric = true;
            option.Password.RequireUppercase = true;
            option.Password.RequiredLength = 6;
            option.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            option.Lockout.MaxFailedAccessAttempts = 5;
            option.Lockout.AllowedForNewUsers = true;

            // User settings.
            option.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            option.User.RequireUniqueEmail = true;
            //option.SignIn.RequireConfirmedEmail = true;

        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
    }
}

