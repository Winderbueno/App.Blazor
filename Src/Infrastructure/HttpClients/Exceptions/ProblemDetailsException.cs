namespace Infrastructure.HttpClients.Exceptions;

public class ProblemDetailsException : Exception
{
    public ProblemDetails? ProblemDetails { get; private set; }

    public ProblemDetailsException(string message) : base(message) { }
}
