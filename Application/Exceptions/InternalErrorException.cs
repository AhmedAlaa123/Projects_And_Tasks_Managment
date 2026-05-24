namespace Application.Exceptions;
public class InternalErrorException : Exception
{
    public InternalErrorException(string message) : base(message) { }
    public InternalErrorException(string message, Exception inner) : base(message, inner) { }
}
