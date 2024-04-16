using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace retailshop.Filters
{
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionHandlingFilter> logger;   
        public ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> logger)
        {
            this.logger = logger;
        }   
        public void OnException(ExceptionContext context) {
            logger.LogError("Exception handled properly");
            context.Result = new ObjectResult($"{context.Exception.Message}")
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
