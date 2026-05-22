
using Api.Hubs;
using Api.Seeding;
using Application;
using Application.Contracts;
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
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            // Add JWT Bearer definition
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
});
        });


        // jwt authentication

        var jwtSettings = new JwtSettings();
        var configureation = builder.Configuration;
        configureation.GetSection(nameof(jwtSettings)).Bind(jwtSettings);


        builder.Services.AddSingleton(jwtSettings);
        // configure identity users
        builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(option =>
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

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
       .AddJwtBearer(x =>
       {
           x.RequireHttpsMetadata = false;
           x.SaveToken = true;
           x.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = jwtSettings.ValidateIssuer,
               ValidIssuers = new[] { jwtSettings.Issuer },
               ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
               ValidAudience = jwtSettings.Audience,
               ValidateAudience = jwtSettings.ValidateAudience,
               ValidateLifetime = jwtSettings.ValidateLifeTime,
           };
       });

        //register mediator
        builder.Services.AddApplication();


        //    .AddMediatR(cfg => {

        //    var assemblies = new[]
        //    {
        //     Assembly.GetExecutingAssembly(),              // Web layer
        //        typeof(Application.Features.Roles.Queries.RoleQueryHandler).Assembly,
        //        typeof(Application.Features.Roles.Command.CreateRoleCommandHandler).Assembly,
        //        typeof(Application.Features.Users.Commands.CreateUserCommandHandler).Assembly,
        //        //typeof(Application.Features.Roles.Queries.RoleQueryHandler).Assembly,

        //    };
        //    cfg.RegisterServicesFromAssemblies(assemblies);
        //});



        // scopped
        builder.Services.AddScoped(typeof(IReposatory<>), typeof(Reposatory<>));
        builder.Services.AddScoped<AuthService>();
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



        builder.Services.AddSignalR();

        // serilog logger
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateBootstrapLogger(); // <-- Use bootstrap logger
        builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            );
        // seed users
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
        var app = builder.Build();
        using var serviceScrop = app.Services.CreateScope();
        await UsersSeed.SeedAsync(serviceScrop.ServiceProvider.GetService<UserManager<ApplicationUser>>(), serviceScrop.ServiceProvider.GetService<IMediator>());
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

