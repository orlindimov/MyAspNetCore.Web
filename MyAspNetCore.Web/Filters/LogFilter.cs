using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MyAspNetCore.Web.Filters
{
    public class LogFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("Action Method calismadan once");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("action method calistikdan sonra");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("action method sonuc uretilmeden once ");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("action method sonuc uretildikten sonra ");
        }
    }
}
