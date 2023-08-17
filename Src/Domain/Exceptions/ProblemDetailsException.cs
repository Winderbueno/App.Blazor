namespace Domain.Exceptions;

public class ProblemDetailsException : Exception
{
    public ProblemDetailsException(string message) : base(message) { }
}
