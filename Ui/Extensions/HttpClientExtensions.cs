using Ui.Handlers;
using Ui.Services;

namespace Ui.Extensions;

public static class HttpClientExtensions
{
    public static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var baseUrl = configuration["ApiSettings:BaseUrl"]!;

        services.AddTransient<JwtCookieHandler>();

        // ✅ Register all HTTP clients here
        services.AddHttpClientWithHandler<ILoginService, LoginService>(baseUrl);
        services.AddHttpClientWithHandler<IProjectService, ProjectService>(baseUrl);
         services.AddHttpClientWithHandler<ITaskService, TaskService>(baseUrl);
        // Add more services here...

        return services;
    }

    private static IServiceCollection AddHttpClientWithHandler<TInterface, TImplementation>(
        this IServiceCollection services,
        string baseUrl)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddHttpClient<TInterface, TImplementation>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        })
        .AddHttpMessageHandler<JwtCookieHandler>();

        return services;
    }
}
