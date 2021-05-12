using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System.Linq;
using TechshopService.Api.Extensions;
using TechshopService.Api.Filters;
using TechshopService.Api.Models;
using TechshopService.Core.Extensions;
using TechshopService.Infra.Data.Extensions;
using TechshopService.Infra.Logger.Extensions;
using TechshopService.Shared.Extensions;

namespace TechshopService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddShared()
                .AddInfraLogger()
                .AddInfraData(Configuration)
                .AddCore(Configuration)
                .AddHealthChecks().Services
                .AddSwagger()
                .AddApiVersioning(o =>
                {
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                })
                .AddVersionedApiExplorer(o =>
                {
                    o.GroupNameFormat = "'v'VVV";
                    o.SubstituteApiVersionInUrl = true;
                });

            services
                .AddControllers(config =>
                {
                    config.Filters.Add<ContextFilter>();
                    config.Filters.Add<ExceptionFilter>();
                    config.Filters.Add<RequestFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .ConfigureApiBehaviorOptions(o => o.InvalidModelStateResponseFactory = ctx =>
                {
                    var errors = ctx.ModelState.Keys
                        .Select(x => new
                        {
                            Key = x,
                            Values = ctx.ModelState[x],
                            Error = ctx.ModelState[x]?.Errors == null ? null : string.Join("|", ctx.ModelState[x]?.Errors.Select(e => e.ErrorMessage))
                        })
                        .Select(x => new Error
                        {
                            Title = $"validation error on field {x.Key}".Replace(" Data.", " "),
                            Detail = string.IsNullOrWhiteSpace(x.Error) ? "Must be a valid value" : x.Error,
                            StatusCode = StatusCodes.Status400BadRequest
                        });

                    return new BadRequestObjectResult(errors);
                });
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .ConfigureSwagger(Configuration, provider)
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
                    {
                        ResultStatusCodes =
                        {
                            [HealthStatus.Healthy] = StatusCodes.Status200OK,
                            [HealthStatus.Degraded] = StatusCodes.Status200OK,
                            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                        }
                    });
                });
        }
    }
}
