using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TechshopService.Core.Repositories;
using TechshopService.Infra.Data.Repositories;

namespace TechshopService.Infra.Data.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration) =>
            services
                .ApplyConfigs(configuration)
                .AddScoped<IProductRepository, ProductRepository>();

        private static IServiceCollection ApplyConfigs(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddStackExchangeRedisCache(opt =>
                {
                    opt.InstanceName = configuration["AppName"];
                    opt.Configuration = configuration["Redis:ConnectionString"];
                });
    }
}
