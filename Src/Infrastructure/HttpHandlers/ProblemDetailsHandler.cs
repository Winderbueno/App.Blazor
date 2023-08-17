using Domain.Exceptions;
using Newtonsoft.Json;

namespace Infrastructure.HttpHandlers;

public class ProblemDetailsHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var response = await base.SendAsync(request, ct);

        var mediaType = response.Content.Headers.ContentType?.MediaType;
        if (mediaType != null && mediaType.Equals("application/problem+json", StringComparison.InvariantCultureIgnoreCase))
        {
            // /!\ Ideally, ProblemDetails class should come from a .Net library
            // However, as of today, it is only available in 'Microsoft.AspNetCore.Mvc.Core' which is not compatible with Blazor Wasm
            // (See. https://github.com/dotnet/aspnetcore/issues/36970)
            // Todo. Add define an homemade model for ProblemDetails
            // var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(await response.Content.ReadAsStringAsync());

            throw new ProblemDetailsException("Todo");
        }

        return response;
    }
}