using Domain.Exceptions;
using Infrastructure.HttpClients;
using Newtonsoft.Json;

namespace Infrastructure.HttpHandlers;

public class ProblemDetailsHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken ct)
    {
        var resp = await base.SendAsync(req, ct);

        var mediaType = resp.Content.Headers.ContentType?.MediaType;
        if (!resp.IsSuccessStatusCode
            || (mediaType != null && mediaType.Equals("application/problem+json", StringComparison.InvariantCultureIgnoreCase)))
        {
            // For now, ProblemDetails is an homemade class (See class for more details)
            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(await resp.Content.ReadAsStringAsync());
            throw new ProblemDetailsException(problemDetails.Message ?? "");
        }

        return resp;
    }
}