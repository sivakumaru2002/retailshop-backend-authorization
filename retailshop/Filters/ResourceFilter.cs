using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace retailshop.Filters
{
    public class ResourceFilter :Attribute,IResourceFilter
    {
        private readonly ILogger<ResourceFilter> logger;
        public ResourceFilter(ILogger<ResourceFilter> logger)
        {
            this.logger = logger;
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Custom-Header", "Value-Modified-By-Resource-Filter");
            logger.LogInformation($"OnResourceExecuting-{context.ActionDescriptor.DisplayName}");

        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value += "\nCustom Footer Added by Resource Filter";
            }
            logger.LogInformation($"OnResourceExecuted-{context.ActionDescriptor.DisplayName}");
        }
    }
}
