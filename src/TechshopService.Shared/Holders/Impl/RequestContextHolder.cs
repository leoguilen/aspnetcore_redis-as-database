using System;
using System.Diagnostics.CodeAnalysis;

namespace TechshopService.Shared.Holders
{
    [ExcludeFromCodeCoverage]
    public class RequestContextHolder : IRequestContextHolder
    {
        public string AppName { get; set; }

        public Guid RequestId { get; set; }

        public Guid CorrelationId { get; set; }

        public string IpAddress { get; set; }

        public object RequestBody { get; set; }
    }
}
