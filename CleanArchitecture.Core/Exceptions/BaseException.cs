namespace CleanArchitecture.Core.Exceptions
{
    /// <summary>
    /// Base exception for application-specific exceptions
    /// </summary>
    public abstract class BaseException : Exception
    {
        public int StatusCode { get; }
        public string? TraceId { get; set; }

        protected BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        protected BaseException(string message, int statusCode, Exception innerException) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }

    /// <summary>
    /// Exception for bad request (400)
    /// </summary>
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message, 400) { }
        public BadRequestException(string message, Exception innerException) : base(message, 400, innerException) { }
    }

    /// <summary>
    /// Exception for unauthorized access (401)
    /// </summary>
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message = "Unauthorized access") : base(message, 401) { }
    }

    /// <summary>
    /// Exception for forbidden access (403)
    /// </summary>
    public class ForbiddenException : BaseException
    {
        public ForbiddenException(string message = "Forbidden") : base(message, 403) { }
    }

    /// <summary>
    /// Exception for not found (404)
    /// </summary>
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, 404) { }
        public NotFoundException(string resourceName, object key) 
            : base($"Resource '{resourceName}' with key '{key}' was not found.", 404) { }
    }

    /// <summary>
    /// Exception for conflict (409)
    /// </summary>
    public class ConflictException : BaseException
    {
        public ConflictException(string message) : base(message, 409) { }
    }

    /// <summary>
    /// Exception for unprocessable entity / validation errors (422)
    /// </summary>
    public class ValidationException : BaseException
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation errors occurred", 422)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(Dictionary<string, string[]> errors) 
            : base("One or more validation errors occurred", 422)
        {
            Errors = errors;
        }

        public ValidationException(string field, string message) : base("One or more validation errors occurred", 422)
        {
            Errors = new Dictionary<string, string[]> { { field, new[] { message } } };
        }
    }

    /// <summary>
    /// Exception for internal server error (500)
    /// </summary>
    public class InternalServerException : BaseException
    {
        public InternalServerException(string message) : base(message, 500) { }
        public InternalServerException(string message, Exception innerException) : base(message, 500, innerException) { }
    }
}
