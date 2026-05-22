using Microsoft.Extensions.DependencyInjection;
namespace Application;
public static class DependancyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly));
    }
}

