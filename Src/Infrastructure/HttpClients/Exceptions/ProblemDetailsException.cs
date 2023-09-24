namespace Infrastructure.HttpClients.Exceptions;

public class ProblemDetailsException : Exception
{
    public ProblemDetails? ProblemDetails { get; private set; }

    public ProblemDetailsException(ProblemDetails problemDetails) : base(problemDetails.Detail)
        => ProblemDetails = problemDetails;
}
