using Infrastructure.HttpClients;
using Infrastructure.HttpClients.Exceptions;
using K.Blazor.Components.Indicators.Toast;
using Newtonsoft.Json;

namespace Infrastructure.HttpHandlers;

public class ProblemDetailsHandler : DelegatingHandler
{
    private readonly ToasterService _toaster;

    public ProblemDetailsHandler(ToasterService toaster) => _toaster = toaster;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken ct)
    {
        var resp = await base.SendAsync(req, ct);

        var mediaType = resp.Content.Headers.ContentType?.MediaType;
        if (!resp.IsSuccessStatusCode
            || (mediaType != null && mediaType.Equals("application/problem+json", StringComparison.InvariantCultureIgnoreCase)))
        {
            // For now, ProblemDetails is an homemade class (See class for more details)
            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(await resp.Content.ReadAsStringAsync(CancellationToken.None));
            _toaster.AddError(problemDetails.Detail);
            throw new ProblemDetailsException(problemDetails.Detail ?? problemDetails.Title ?? "");
        }

        return resp;
    }
}