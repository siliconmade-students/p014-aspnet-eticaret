namespace Eticaret.Web.Mvc.Middlewares
{
    public class IpLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public IpLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logDirectory = Directory.GetCurrentDirectory() + "\\Logs";

            if (!Directory.Exists(logDirectory)) Directory.CreateDirectory(logDirectory);

            using var sw = File.AppendText(Path.Combine(logDirectory, "Logs.txt"));
            var ip = context.Connection.RemoteIpAddress;
            sw.WriteLine(ip + ";" + DateTime.Now);

            await _next.Invoke(context);
        }
    }

    public static class IpLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseIPLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpLoggingMiddleware>();
        }
    }
}
