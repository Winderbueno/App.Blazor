﻿using Domain.Exceptions;
using Infrastructure.HttpClients;
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
            _toaster.AddError(problemDetails.Message);
            throw new ProblemDetailsException(problemDetails.Message ?? "");
        }

        return resp;
    }
}