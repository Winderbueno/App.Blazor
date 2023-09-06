﻿using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Infrastructure.HttpHandlers;

// https://stackoverflow.com/questions/70337553/globally-apply-setbrowserrequestcredentials
// Usage of 'SetBrowserRequestCredentials' imply needing 'Microsoft.AspNetCore.Components.WebAssembly' package
public class AddCredentialHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        return base.SendAsync(request, cancellationToken);
    }
}