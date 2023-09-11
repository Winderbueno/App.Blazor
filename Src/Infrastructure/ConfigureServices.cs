using Domain.Configuration;
using Infrastructure.HttpClients;
using Infrastructure.HttpClients.Shop;
using Infrastructure.HttpHandlers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, RootConf rootConf, string baseUrl)
    {
        #region Http Handlers
        services.AddScoped<AddCredentialHandler>()
                .AddScoped<AddTokenHandler>()
                .AddScoped<ProblemDetailsHandler>();
        #endregion   

        #region Http Clients
        // Local client pointing in /wwwroot (Used for demo only)
        services.AddHttpClient(HttpClientName.LocalClient, client => client.BaseAddress = new Uri(baseUrl));

        // Shop.Api
        services.AddHttpClient<IShopApi, ShopApi>(client => { client.BaseAddress = new Uri(rootConf.ShopApiUrl); })
            .AddHttpMessageHandler<AddCredentialHandler>()
            .AddHttpMessageHandler<AddTokenHandler>()
            .AddHttpMessageHandler<ProblemDetailsHandler>();
        #endregion     

        return services;
    }
}