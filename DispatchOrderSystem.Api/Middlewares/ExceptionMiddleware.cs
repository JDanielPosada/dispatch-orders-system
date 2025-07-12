using FluentValidation;
using System.Net;
using System.Text.Json;

namespace DispatchOrderSystem.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = GetStatusCode(ex);

                var result = new
                {
                    status = context.Response.StatusCode,
                    message = "An unexpected error occurred.",
                    detail = ex.Message
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        }

        private static int GetStatusCode(Exception ex) =>
            ex switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
    }
}
