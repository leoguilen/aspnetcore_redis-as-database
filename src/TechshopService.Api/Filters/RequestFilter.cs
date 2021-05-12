using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using TechshopService.Infra.Logger.Logging;

namespace TechshopService.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class RequestFilter : IActionFilter
    {
        private readonly ILogWriter _logWriter;

        public RequestFilter(ILogWriter logWriter) => _logWriter = logWriter;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controllerName = context.Controller.GetType().FullName;
            _logWriter.Info($"Executed {controllerName}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.Controller.GetType().FullName;
            _logWriter.Info($"Executing {controllerName}");
        }
    }
}
