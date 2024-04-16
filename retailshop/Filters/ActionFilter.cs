using Microsoft.AspNetCore.Mvc.Filters;

namespace retailshop.Filters
{
    public class ActionFilter : Attribute, IActionFilter { 

        private readonly ILogger<ActionFilter> logger;
        public ActionFilter(ILogger<ActionFilter> _logger )
        {
            this.logger = _logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("OnActionExecuting");

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("OnActionExecuted");
        }
    }
}
