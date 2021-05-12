using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TechshopService.Core.Notifications;
using TechshopService.Core.Services;

namespace TechshopService.Core.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration _) =>
            services
                .AddScoped<INotification, Notification>()
                .AddScoped<IProductService, ProductService>();
    }
}
