using Microsoft.AspNetCore.Mvc.Filters;

namespace Eticaret.Web.Mvc.Filters
{
    public class SampleActionFilter : ActionFilterAttribute //Attribute, IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Executed");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Executing");
        }
    }
}
