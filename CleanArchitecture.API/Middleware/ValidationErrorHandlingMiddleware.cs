using System.Text.Json;
using CleanArchitecture.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.API.Middleware
{
    /// <summary>
    /// Middleware to handle validation errors from ModelState
    /// </summary>
    public class ValidationErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidationErrorHandlingMiddleware> _logger;

        public ValidationErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ValidationErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            // Check if response is a BadRequest with ModelState errors
            if (context.Response.StatusCode == 400)
            {
                var endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    // ModelState validation is typically handled by ApiController attribute
                    // This middleware can be extended for additional validation scenarios
                }
            }
        }
    }
}
