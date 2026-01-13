using CleanArchitecture.Core.Common;
using CleanArchitecture.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    /// <summary>
    /// Demo controller to showcase error handling capabilities
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ErrorHandlingDemoController : ControllerBase
    {
        /// <summary>
        /// Example: Bad Request (400)
        /// </summary>
        [HttpGet("bad-request")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public IActionResult BadRequestExample()
        {
            throw new BadRequestException("This is a bad request example");
        }

        /// <summary>
        /// Example: Unauthorized (401)
        /// </summary>
        [HttpGet("unauthorized")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public IActionResult UnauthorizedExample()
        {
            throw new UnauthorizedException("You are not authorized to access this resource");
        }

        /// <summary>
        /// Example: Forbidden (403)
        /// </summary>
        [HttpGet("forbidden")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        public IActionResult ForbiddenExample()
        {
            throw new ForbiddenException("You don't have permission to perform this action");
        }

        /// <summary>
        /// Example: Not Found (404)
        /// </summary>
        [HttpGet("not-found")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public IActionResult NotFoundExample()
        {
            throw new NotFoundException("Resource", "12345");
        }

        /// <summary>
        /// Example: Conflict (409)
        /// </summary>
        [HttpGet("conflict")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public IActionResult ConflictExample()
        {
            throw new ConflictException("The resource already exists");
        }

        /// <summary>
        /// Example: Validation Error (422) with field-level details
        /// </summary>
        [HttpPost("validation-error")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status422UnprocessableEntity)]
        public IActionResult ValidationErrorExample([FromBody] ValidationRequest request)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Email", new[] { "Email is required", "Email format is invalid" } },
                { "Age", new[] { "Age must be between 18 and 100" } },
                { "Name", new[] { "Name cannot be empty" } }
            };

            throw new ValidationException(errors);
        }

        /// <summary>
        /// Example: Internal Server Error (500)
        /// </summary>
        [HttpGet("internal-server-error")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult InternalServerErrorExample()
        {
            throw new InternalServerException("An unexpected error occurred");
        }

        /// <summary>
        /// Example: Generic Exception (500)
        /// </summary>
        [HttpGet("generic-exception")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GenericExceptionExample()
        {
            throw new InvalidOperationException("This is a generic exception that will be handled");
        }

        /// <summary>
        /// Example: Success Response
        /// </summary>
        [HttpGet("success")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public IActionResult SuccessExample()
        {
            return Ok(ApiResponse<string>.SuccessResponse("Hello World", "Operation completed successfully"));
        }
    }

    /// <summary>
    /// Request model for validation example
    /// </summary>
    public class ValidationRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
    }
}
