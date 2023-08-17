namespace Infrastructure.HttpClients;

public partial class ApiException : Exception
{
    public int StatusCode { get; private set; }

    public string Response { get; private set; }

    public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }


    public ApiException(string message, int statusCode, string response, IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception innerException)
        : base(BuildMessage(message, statusCode, response), innerException)
    {
        StatusCode = statusCode;
        Response = response;
        Headers = headers;
    }

    private static string BuildMessage(string message, int statusCode, string response)
    {
        return message
              + "\n\nStatus: " + statusCode
                + "\nResponse: \n" + (
                    response == null ? "(null)" : response.Substring(0, Math.Min(response.Length, 512))
              );
    }

    public override string ToString()
    {
        return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
    }
}

public partial class ApiException<TResult> : ApiException
{
    public TResult Result { get; private set; }

    public ApiException(string message, int statusCode, string response, IReadOnlyDictionary<string, IEnumerable<string>> headers, TResult result, Exception innerException)
        : base(message, statusCode, response, headers, innerException)
    {
        Result = result;
    }
}
