using System.Net;
using System.Text.Json;
using CleanArchitecture.Core.Common;
using CleanArchitecture.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.API.Middleware
{
    /// <summary>
    /// Global exception handling middleware for consistent error responses
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlingMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = context.TraceIdentifier;
            var response = context.Response;
            response.ContentType = "application/json";

            ApiResponse apiResponse;
            
            if (exception is ValidationException validationEx)
            {
                apiResponse = HandleValidationException(validationEx, traceId);
            }
            else if (exception is BaseException baseException)
            {
                apiResponse = HandleBaseException(baseException, traceId);
            }
            else if (exception is ArgumentException argEx)
            {
                apiResponse = HandleArgumentException(argEx, traceId);
            }
            else if (exception is ArgumentNullException argNullEx)
            {
                apiResponse = HandleArgumentNullException(argNullEx, traceId);
            }
            else if (exception is KeyNotFoundException keyNotFoundEx)
            {
                apiResponse = HandleKeyNotFoundException(keyNotFoundEx, traceId);
            }
            else if (exception is UnauthorizedAccessException unauthorizedEx)
            {
                apiResponse = HandleUnauthorizedAccessException(unauthorizedEx, traceId);
            }
            else if (exception is InvalidOperationException invalidOpEx)
            {
                apiResponse = HandleInvalidOperationException(invalidOpEx, traceId);
            }
            else
            {
                apiResponse = HandleGenericException(exception, traceId);
            }

            response.StatusCode = apiResponse.StatusCode;

            // Log exception
            LogException(exception, apiResponse.StatusCode, traceId);

            // Serialize response
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _environment.IsDevelopment()
            };

            var jsonResponse = JsonSerializer.Serialize(apiResponse, jsonOptions);
            await response.WriteAsync(jsonResponse);
        }

        private ApiResponse HandleValidationException(ValidationException exception, string traceId)
        {
            exception.TraceId = traceId;
            var errors = ConvertValidationErrors(exception.Errors);

            return ApiResponse.ErrorResponse(
                message: exception.Message,
                statusCode: exception.StatusCode,
                errors: errors,
                traceId: traceId
            );
        }

        private ApiResponse HandleBaseException(BaseException exception, string traceId)
        {
            exception.TraceId = traceId;

            return ApiResponse.ErrorResponse(
                message: exception.Message,
                statusCode: exception.StatusCode,
                errors: null,
                traceId: traceId
            );
        }

        private ApiResponse HandleArgumentException(ArgumentException exception, string traceId)
        {
            return ApiResponse.ErrorResponse(
                message: exception.Message,
                statusCode: 400,
                traceId: traceId
            );
        }

        private ApiResponse HandleArgumentNullException(ArgumentNullException exception, string traceId)
        {
            return ApiResponse.ErrorResponse(
                message: exception.Message ?? "A required parameter was null",
                statusCode: 400,
                traceId: traceId
            );
        }

        private ApiResponse HandleKeyNotFoundException(KeyNotFoundException exception, string traceId)
        {
            return ApiResponse.ErrorResponse(
                message: exception.Message,
                statusCode: 404,
                traceId: traceId
            );
        }

        private ApiResponse HandleUnauthorizedAccessException(UnauthorizedAccessException exception, string traceId)
        {
            return ApiResponse.ErrorResponse(
                message: "Unauthorized access",
                statusCode: 401,
                traceId: traceId
            );
        }

        private ApiResponse HandleInvalidOperationException(InvalidOperationException exception, string traceId)
        {
            return ApiResponse.ErrorResponse(
                message: exception.Message,
                statusCode: 400,
                traceId: traceId
            );
        }

        private ApiResponse HandleGenericException(Exception exception, string traceId)
        {
            var message = _environment.IsDevelopment()
                ? exception.Message
                : "An error occurred while processing your request. Please try again later.";

            return ApiResponse.ErrorResponse(
                message: message,
                statusCode: 500,
                traceId: traceId
            );
        }

        private List<ErrorDetail>? ConvertValidationErrors(Dictionary<string, string[]> errors)
        {
            if (errors == null || errors.Count == 0)
                return null;

            return errors.SelectMany(kvp =>
                kvp.Value.Select(error => new ErrorDetail(kvp.Key, error))
            ).ToList();
        }

        private void LogException(Exception exception, int statusCode, string traceId)
        {
            var logLevel = statusCode >= 500 ? LogLevel.Error : LogLevel.Warning;

            _logger.Log(
                logLevel,
                exception,
                "Exception occurred. StatusCode: {StatusCode}, TraceId: {TraceId}, Message: {Message}",
                statusCode,
                traceId,
                exception.Message
            );

            // Log full exception details in development
            if (_environment.IsDevelopment() && statusCode >= 500)
            {
                _logger.LogError(exception, "Full exception details for TraceId: {TraceId}", traceId);
            }
        }
    }
}
