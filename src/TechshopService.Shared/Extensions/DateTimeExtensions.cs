using System;

namespace TechshopService.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime actualDateTime) =>
            actualDateTime.ToUniversalTime();
    }
}
