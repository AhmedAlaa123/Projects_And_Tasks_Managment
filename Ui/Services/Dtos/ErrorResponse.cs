namespace Ui.Services.Dtos;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public List<ValidationError> Errors { get; set; } = new();

    public ErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
    public ErrorResponse(int statusCode, string message, List<ValidationError> errors) : this(statusCode, message) => Errors = errors;

    /// <summary>
    /// this class has validtion errors for feild and message
    /// </summary>
    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
