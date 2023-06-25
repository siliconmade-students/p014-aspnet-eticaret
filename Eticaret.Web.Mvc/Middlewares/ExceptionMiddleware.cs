using System.Net;

namespace Eticaret.Web.Mvc.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //context.Response.ContentType = "application/json";

            _logger.LogError($"Something went wrong: {exception.Message}");
            // DB
            // Email

            //await context.Response.WriteAsync("Internal Server Error.");
            context.Response.Redirect("/Home/Error");
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void UseGlobalExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }

}
