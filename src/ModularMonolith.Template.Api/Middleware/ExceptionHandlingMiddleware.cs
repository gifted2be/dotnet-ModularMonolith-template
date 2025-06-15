using ModularMonolith.Template.SharedKernel.DTOs;
using ModularMonolith.Template.SharedKernel.Exceptions;
using ModularMonolith.Template.SharedKernel.Helpers;

namespace ModularMonolith.Template.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex) {
                _logger.LogWarning(ex, "Application error occurred");

                BaseResponse<object> errorResponse = ResponseHelper.Fail<object>(ex.Message, (int)ex.ErrorCode);

                context.Response.StatusCode = (int)ex.ErrorCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Domain error occurred");

                BaseResponse<object> errorResponse = ResponseHelper.Fail<object>(ex.Message, (int)ex.ErrorCode);

                context.Response.StatusCode = (int)ex.ErrorCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error occurred");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Internal Server Error"
                });
            }
        }
    }

}
