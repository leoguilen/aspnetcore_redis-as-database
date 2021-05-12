using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TechshopService.Shared.Holders;

namespace TechshopService.Shared.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services) =>
            services
                .AddScoped<IRequestContextHolder, RequestContextHolder>();
    }
}
