using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TechshopService.Infra.Logger.Logging;

namespace TechshopService.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class ContextFilter : IActionFilter
    {
        private readonly string _appName;
        private readonly ILogWriter _logWriter;

        public ContextFilter(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // not used
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var requestId = Guid.NewGuid();
            var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
            var correlationId = Guid.TryParse(httpContext.Request.Headers["correlation_id"], out Guid guid)
                ? guid
                : Guid.NewGuid();

            _logWriter.RequestId = requestId;
            _logWriter.CorrelationId = correlationId;
            _logWriter.RequestBody = ReadBody(context);
            _logWriter.IpAddress = ipAddress;
        }

        private static object ReadBody(ActionExecutingContext context)
        {
            var parameters = context.ActionDescriptor.Parameters;
            var bodyParameter = ResolveBodyParameterName(parameters);

            return context.ActionArguments.TryGetValue(bodyParameter, out var body) ? body : default;
        }

        private static string ResolveBodyParameterName(IList<ParameterDescriptor> parameters)
        {
            var parameter = parameters.FirstOrDefault(p =>
            {
                var type = p.ParameterType;
                return type.Name.Contains("Request");
            });

            return parameter is null
                ? string.Empty
                : parameter.Name;
        }
    }
}
