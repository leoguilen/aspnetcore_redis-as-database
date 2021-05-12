using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.CodeAnalysis;
using TechshopService.Api.Models;
using TechshopService.Infra.Logger.Logging;
using TechshopService.Shared.Holders;

namespace TechshopService.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IRequestContextHolder _requestContextHolder;
        private readonly ILogWriter _logWriter;

        public ExceptionFilter(
            IRequestContextHolder requestContextHolder,
            ILogWriter logWriter)
        {
            _requestContextHolder = requestContextHolder;
            _logWriter = logWriter;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var error = Error.FromException(ex);

            LogException(ex, error);

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(error);
            context.HttpContext.Response.StatusCode = error.StatusCode;
        }

        private void LogException(Exception ex, Error error)
        {
            var message = error.Detail ?? ex.Message;
            var data = _requestContextHolder.RequestBody;

            _logWriter.Error(message, data, ex, ex.TargetSite?.Name);
        }
    }
}
