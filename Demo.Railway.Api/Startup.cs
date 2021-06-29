using System.Text.Json;
using System.Text.Json.Serialization;
using Demo.Railway.Abstraction.Repositories;
using Demo.Railway.Abstraction.Services;
using Demo.Railway.Core.Repositories;
using Demo.Railway.Core.Services;
using HealthChecks.UI.Client;
using Jpn.Authorization.B2C.Options;
using Jpn.Utilities.AspNetCore.Filters;
using Jpn.Utilities.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Demo.Railway.Api
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">The service's configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The service's configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ILightRepository, LightRepository>()
                .AddSingleton<ILightService, LightService>()
                .AddSingleton<IGatewayRepository, GatewayRepository>()
                .AddSingleton<IGatewayService, GatewayService>();

            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] {"critical"});

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(ProblemDetailsFilter));
                    options.Filters.Add(typeof(ExceptionHandlerFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services
                .AddApiVersioning()
                .AddVersionedApiExplorer()
                .AddSwaggerGen(options =>
                {
                    options.AddDefaultSwaggerConfiguration(services, "Demo.Railway.Api");
                });
        }

        /// <summary>
        /// Configure service.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The <see cref="IWebHostEnvironment"/>.</param>
        /// <param name="clientOptions">The <see cref="IOptions{TOptions}"/> of <see cref="DefaultClientOptions"/>.</param>
        /// <param name="apiVersionDescriptionProvider">The <see cref="IApiVersionDescriptionProvider"/>.</param>
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IOptions<DefaultClientOptions> clientOptions,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.AddVersionedEndpoints(apiVersionDescriptionProvider, "Demo.Railway.Api");
                });
            }

            app.UseApiVersioning();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapSwagger();
            });
        }
    }
}
