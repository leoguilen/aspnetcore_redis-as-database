using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace TechshopService.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class Error
    {
        public string Title { get; init; }

        public string Detail { get; init; }

        public int StatusCode { get; init; }

        public static Error FromException(Exception exception) => new()
        {
            Title = "Application error",
            Detail = exception.Message,
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }
}
