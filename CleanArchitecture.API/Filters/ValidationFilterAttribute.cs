using CleanArchitecture.Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.API.Filters
{
    /// <summary>
    /// Action filter to handle ModelState validation errors
    /// </summary>
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .SelectMany(kvp =>
                        kvp.Value!.Errors.Select(error => new ErrorDetail(
                            field: kvp.Key,
                            message: error.ErrorMessage,
                            value: kvp.Value.AttemptedValue
                        ))
                    )
                    .ToList();

                var response = ApiResponse.ErrorResponse(
                    message: "Validation failed",
                    statusCode: 422,
                    errors: errors,
                    traceId: context.HttpContext.TraceIdentifier
                );

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 422
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
