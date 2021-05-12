using System;
using System.Runtime.CompilerServices;

namespace TechshopService.Infra.Logger.Logging
{
    public interface ILogWriter : IDisposable
    {
        public Guid RequestId { get; set; }

        public Guid CorrelationId { get; set; }

        public object RequestBody { get; set; }

        public string IpAddress { get; set; }

        void Info(string message, object data = default, Exception ex = default,
            [CallerMemberName] string source = "");

        void Warn(string message, object data = default, Exception ex = default,
            [CallerMemberName] string source = "");

        void Error(string message, object data = default, Exception ex = default,
            [CallerMemberName] string source = "");

        void Fatal(string message, object data = default, Exception ex = default,
            [CallerMemberName] string source = "");
    }
}
