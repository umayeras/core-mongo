using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebApp.Data.Constants;
using WebApp.Model.Constants;

namespace WebApp.Extensions
{
    /// <summary>
    /// Health Monitor UI - /healthchecks-ui
    /// </summary>
    internal static class HealthCheckExtension
    {
        internal static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbSettings = configuration.GetSection(SectionNames.MongoDbSettings).Get<MongoDbSettings>();
            
            services.AddHealthChecks()
                .AddMongoDb(mongodbConnectionString: mongoDbSettings.Connection,
                    name: mongoDbSettings.Database,
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] {"db", "mongo-db"});

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15);
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);
                opt.AddHealthCheckEndpoint(nameof(WebApp), "/health");
            }).AddInMemoryStorage();
        }

        internal static void MapHealthCheck(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecksUI();
        }
    }
}