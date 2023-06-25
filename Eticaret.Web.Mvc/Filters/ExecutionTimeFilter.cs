using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Eticaret.Web.Mvc.Filters
{
    public class ExecutionTimeFilter : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopwatch.Stop();
            var executionTime = stopwatch.ElapsedMilliseconds;

            var controllerName = filterContext.Controller.GetType().Name;
            var actionName = filterContext.ActionDescriptor.DisplayName;

            Console.WriteLine($"Controller: {controllerName}, Action: {actionName}, Execution Time: {executionTime} ms");
            if (executionTime > 500)
            {
                // Log Db
                // Email
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
