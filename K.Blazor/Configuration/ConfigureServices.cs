using K.Blazor.Components.Indicators.Toast;
using K.Blazor.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddKBlazorServices(this IServiceCollection services)
    {
        services.AddSingleton<ToasterService>()
                .AddSingleton<KConfService>();

        return services;
    }
}