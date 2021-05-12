using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TechshopService.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Techshop.Service-Api",
                    Description = "Web Api (.NET 5)",
                    Version = "v1"
                });

                opt.ExampleFilters();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                opt.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(
            this IApplicationBuilder app,
            IConfiguration configuration,
            IApiVersionDescriptionProvider provider)
        {
            var useSwagger = configuration.GetValue<bool>("AppSettings:UseSwagger");

            if (useSwagger is false)
            {
                return app;
            }

            return app
                .UseSwagger()
                .UseSwaggerUI(o => provider.ApiVersionDescriptions
                    .ToList()
                    .ForEach(d =>
                        o.SwaggerEndpoint($"/swagger/{d.GroupName}/swagger.json", d.GroupName.ToString())));
        }
    }
}
