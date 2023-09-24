using Infrastructure.HttpClients;
using Infrastructure.HttpClients.Exceptions;
using K.Blazor.Components.Indicators.Toast;
using Newtonsoft.Json;
using System.Net;

namespace Infrastructure.HttpHandlers;

public class ProblemDetailsHandler : DelegatingHandler
{
    private readonly ToasterService _toaster;

    public ProblemDetailsHandler(ToasterService toaster) => _toaster = toaster;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken ct)
    {
        var resp = await base.SendAsync(req, ct);

        // Standard Handling for Api Business Errors
        var mediaType = resp.Content.Headers.ContentType?.MediaType;
        if (HttpStatusCode.BadRequest.Equals(resp.StatusCode)
            || HttpStatusCode.NotFound.Equals(resp.StatusCode)
            || (mediaType != null && mediaType.Equals("application/problem+json", StringComparison.InvariantCultureIgnoreCase)))
        {
            // For now, ProblemDetails is an homemade class (See class for more details)
            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(await resp.Content.ReadAsStringAsync(CancellationToken.None));
            _toaster.AddError(problemDetails.Detail ?? problemDetails.Title);
            throw new ProblemDetailsException(problemDetails);
        }

        return resp;
    }
}