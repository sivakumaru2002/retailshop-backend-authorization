using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace retailshop.Filters
{
    public class ResultFilter : IResultFilter
    {
        private readonly ILogger<ResultFilter> logger;
        public ResultFilter(ILogger<ResultFilter> logger)
        {
            this.logger = logger;
        }
        public void OnResultExecuting(ResultExecutingContext context)
        {
           
            logger.LogInformation("OnResultExecuting");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            logger.LogInformation("OnResultExecuted");
        }
    }
}
