using Microsoft.Extensions.Configuration;
using RentCarCommand.Infra.Logger.Models;
using Serilog;
using Serilog.Events;
using System;
using System.Runtime.CompilerServices;

namespace TechshopService.Infra.Logger.Logging
{
    public class LogWriter : ILogWriter
    {
        private readonly string _appName;

        private ILogger _logger;
        private bool _disposed;

        public LogWriter(
            ILogger logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _appName = configuration["AppSettings:AppName"];
        }

        public Guid RequestId { get; set; }

        public Guid CorrelationId { get; set; }

        public object RequestBody { get; set; }

        public string IpAddress { get; set; }

        public void Error(string message, object data = null, Exception ex = null, [CallerMemberName] string source = "")
        {
            Log(message, data, ex, source, LogEventLevel.Error, _logger.Error);
        }

        public void Fatal(string message, object data = null, Exception ex = null, [CallerMemberName] string source = "")
        {
            Log(message, data, ex, source, LogEventLevel.Fatal, _logger.Fatal);
        }

        public void Info(string message, object data = null, Exception ex = null, [CallerMemberName] string source = "")
        {
            Log(message, data, ex, source, LogEventLevel.Information, _logger.Information);
        }

        public void Warn(string message, object data = null, Exception ex = null, [CallerMemberName] string source = "")
        {
            Log(message, data, ex, source, LogEventLevel.Warning, _logger.Warning);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Log(
            string message,
            object data,
            Exception ex,
            string source,
            LogEventLevel level,
            Action<string, LogMessage> logger)
        {
            var logMessage = new LogMessage
            {
                Application = _appName,
                Data = data ?? RequestBody,
                Level = level.ToString(),
                Message = message,
                Method = source,
                Timestamp = DateTime.Now,
                RequestId = RequestId.ToString(),
                CorrelationId = CorrelationId.ToString(),
                IpAddress = IpAddress,
            };

            if (ex is not null)
            {
                logMessage.StackTrace = ex.StackTrace;
            }

            logger.Invoke("{@LogMessage}", logMessage);
        }

        ~LogWriter()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            _logger = null;
            _disposed = true;
        }
    }
}
