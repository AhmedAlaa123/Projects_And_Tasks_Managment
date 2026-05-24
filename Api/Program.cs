
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Api.DependancyInjection;
using Api.Hubs;
using Api.Middlewares;
using Api.Seeding;
using Application.Contracts;
using Application.Dependancies;
using Application.Reposatories;
using Application.Services;
using Domain.Models;
using Domain.Utilities;
using InfraStructure.context;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Api;

public class Program
{
    public static async System.Threading.Tasks.Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString(Constants.CONNECTION_STRING_KEY_NAME);
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, serviceOptions =>
            {
                serviceOptions.CommandTimeout(Constants.SQL_COMMAND_TIMEOUT);

            });
        });
        builder.Services.AddControllers();
        builder.Services.AddServices(builder.Configuration);
        // cors policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                policy =>
                {
                    policy.WithOrigins("https://localhost:7085") // your client origin
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();

                });
        });
        // Register Signal R To Realtime Update
        builder.Services.AddSignalR();
        // add loging monotoring
        builder.Host.AddLogger();
        #region Seeding 
        // seed users
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
        var app = builder.Build();
        using var serviceScrop = app.Services.CreateScope();
        await UsersSeed.SeedAsync(serviceScrop.ServiceProvider.GetService<UserManager<ApplicationUser>>(), serviceScrop.ServiceProvider.GetService<IMediator>());
        #endregion
        // configuer golbal exception medilware
        app.UseMiddleware<GlobalExceptionMiddleware>();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("AllowSpecificOrigins");
        app.MapHub<NotificationHub>("/notifyHub");
        app.UseHttpsRedirection();
        app.UseAuthentication(); // this for authenticate users
        app.UseAuthorization(); // authorize users
        app.MapControllers();
        app.Run();
    }
}

