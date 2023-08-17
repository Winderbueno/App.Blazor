using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace Ui.Core.Authorization;

public class ConfigureAuthorization : IConfigureOptions<AuthorizationOptions>
{
    public ConfigureAuthorization() { }

    public void Configure(AuthorizationOptions options)
    {
        // Define Crud policy for each entity
        List<string> entity = new() { "user", "client", "contract" };
        entity.ForEach(x => options.AddCrudPolicy(x));
    }
}