using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Eticaret.Web.Mvc.Middlewares
{
    public static class ExceptionExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    //context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        // DB
                        // Email

                        //await context.Response.WriteAsync("Internal Server Error.");
                        context.Response.Redirect("/Home/Error");
                    }

                    return Task.CompletedTask;
                });
            });
        }
    }
}
