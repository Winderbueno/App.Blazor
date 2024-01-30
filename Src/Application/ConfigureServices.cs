using Application.Services;
using Application.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IAuthService, AuthService>()
                .AddScoped<IDicoService, DicoService>()
                .AddScoped<IUserService, UserService>();

        return services;
    }
}