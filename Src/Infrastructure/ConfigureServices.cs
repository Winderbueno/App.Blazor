using Infrastructure.HttpClients;
using Infrastructure.HttpClients.Shop;
using Infrastructure.HttpHandlers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string baseUrl)
    {
        #region Http Clients
        // Local client pointing in /wwwroot (Used for demo only)
        services.AddHttpClient(HttpClientName.LocalClient, client => client.BaseAddress = new Uri(baseUrl));

        // Shop.Api
        services.AddHttpClient<IShopApi, ShopApi>(client => { client.BaseAddress = new Uri("https://shoppinglistappback.azurewebsites.net"); })
            .AddHttpMessageHandler<ProblemDetailsHandler>();
        #endregion

        #region Http Handlers
        services.AddScoped<ProblemDetailsHandler>();
        #endregion        

        return services;
    }
}