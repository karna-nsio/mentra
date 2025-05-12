using System.Net;
using System.Text.Json;
using letstrydotnetemp.Exceptions;

namespace letstrydotnetemp.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
            var response = context.Response;
            response.ContentType = "application/json";
            
            var errorResponse = new
            {
                Message = exception.Message,
                StatusCode = GetStatusCode(exception)
            };

            response.StatusCode = (int)errorResponse.StatusCode;
            
            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);
            
            await response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        private HttpStatusCode GetStatusCode(Exception exception)
        {
            return exception switch
            {
                TaskNotFoundException => HttpStatusCode.NotFound,
                UnauthorizedTaskOperationException => HttpStatusCode.Forbidden,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
} 