using Microsoft.AspNetCore.Mvc.Filters;

namespace retailshop.Filters
{
    public class ActionFilter2 : Attribute , IActionFilter
    {
        private readonly ILogger<ActionFilter2> _logger;
        public ActionFilter2(ILogger<ActionFilter2> logger)
        {
            _logger = logger;
        } 
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("ActionFilter2Executing");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("ActionFilter2Executed");
        }
    }
}
