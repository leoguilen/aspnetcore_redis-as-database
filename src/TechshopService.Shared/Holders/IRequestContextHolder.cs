using System;

namespace TechshopService.Shared.Holders
{
    public interface IRequestContextHolder
    {
        public string AppName { get; set; }

        public Guid RequestId { get; set; }

        public Guid CorrelationId { get; set; }

        public string IpAddress { get; set; }

        public object RequestBody { get; set; }
    }
}
