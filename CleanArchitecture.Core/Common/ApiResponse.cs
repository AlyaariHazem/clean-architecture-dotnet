namespace CleanArchitecture.Core.Common
{
    /// <summary>
    /// Standard API response wrapper for consistent response structure
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<ErrorDetail>? Errors { get; set; }
        public string TraceId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation completed successfully", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, int statusCode, List<ErrorDetail>? errors = null, string? traceId = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors,
                TraceId = traceId ?? string.Empty
            };
        }
    }

    /// <summary>
    /// Non-generic API response for operations without data
    /// </summary>
    public class ApiResponse : ApiResponse<object>
    {
        public static ApiResponse SuccessResponse(string message = "Operation completed successfully", int statusCode = 200)
        {
            return new ApiResponse
            {
                Success = true,
                StatusCode = statusCode,
                Message = message
            };
        }

        public static new ApiResponse ErrorResponse(string message, int statusCode, List<ErrorDetail>? errors = null, string? traceId = null)
        {
            return new ApiResponse
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors,
                TraceId = traceId ?? string.Empty
            };
        }
    }

    /// <summary>
    /// Error detail for field-level validation errors
    /// </summary>
    public class ErrorDetail
    {
        public string Field { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? Value { get; set; }

        public ErrorDetail(string field, string message, object? value = null)
        {
            Field = field;
            Message = message;
            Value = value;
        }
    }
}
