using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using TechshopService.Infra.Logger.Logging;

namespace TechshopService.Infra.Logger.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraLogger(this IServiceCollection services) =>
            services
                .ConfigLogger()
                .AddScoped<ILogWriter, LogWriter>();

        private static IServiceCollection ConfigLogger(this IServiceCollection services) =>
            services.AddSingleton<ILogger>(_ =>
                new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}")
                    .CreateLogger());
    }
}
