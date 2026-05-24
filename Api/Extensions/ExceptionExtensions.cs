namespace Api.Extensions;

public static class ExceptionExtensions
{
    public static string GetExceptionMessage(this Exception exception)
    {
        if (exception.InnerException == null)
        {
            return exception.Message;
        }
        return exception.InnerException.Message;
    }  
}
