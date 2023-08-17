using Ui;
using Ui.Core.Authorization;
using Ui.Core.Translations;
using Ui.Shared.Indicators.Toast;
using I18NPortable;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var conf = builder.Configuration;
var services = builder.Services;

#region App 
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
#endregion    

#region Authentication
// Todo
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

#region Error Handling
services.AddSingleton<ToasterService>();
#endregion

#region I18n
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
services.AddInfrastructureServices(builder.HostEnvironment.BaseAddress);
#endregion

await builder.Build().RunAsync();