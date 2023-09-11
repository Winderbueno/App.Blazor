using Blazored.LocalStorage;
using Domain.Configuration;
using I18NPortable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Presentation;
using Presentation.Middlewares.Authentication;
using Presentation.Middlewares.Authorization;
using Presentation.Middlewares.Globalization;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;
var conf = builder.Configuration;

#region App
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
#endregion

#region Authentication
services.AddScoped<TokenStorage>()
        .AddScoped<AuthStateProvider>()
        .AddScoped<AuthenticationStateProvider>(provider =>
            provider.GetRequiredService<AuthStateProvider>());
#endregion

#region Authorization
services.AddAuthorizationCore();
services.AddSingleton<IConfigureOptions<AuthorizationOptions>, ConfigureAuthorization>();
#endregion

#region Logging
// Serilog. https://github.com/serilog/serilog/wiki/Configuration-Basics
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(conf) // https://github.com/serilog/serilog-settings-configuration
    .CreateLogger();
#endregion

#region Globalization
// I18N-Portable. https://github.com/xleon/I18N-Portable
services.AddSingleton((_) =>
        I18N.Current
            .AddLocaleReader(new JsonReader(), ".json")
            .SetFallbackLocale("fr")
            .SetNotFoundSymbol("##")
            .Init(typeof(Program).Assembly));
services.AddSingleton(new LanguageEvents());
#endregion

#region Project Services
services.AddApplicationServices();
services.AddInfrastructureServices(conf.Get<RootConf>(), builder.HostEnvironment.BaseAddress);
services.AddKBlazorServices();
services.AddBlazoredLocalStorage();
#endregion

await builder.Build().RunAsync();