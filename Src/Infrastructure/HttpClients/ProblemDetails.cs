namespace Infrastructure.HttpClients;

/* 
 * /!\ Ideally, ProblemDetails class, as it is a standard for ApiErrorResponse,
 *      could be integrated in a .Net library. However, as of today, it is only
 *      available in 'Microsoft.AspNetCore.Mvc.Core' which is not compatible with Blazor Wasm
 *      (See. https://github.com/dotnet/aspnetcore/issues/36970)
 **/
public class ProblemDetails
{
    public string? Message;
}