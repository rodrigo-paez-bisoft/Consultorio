using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Bisoft.Consultorio.Api.Extensions.Endponints
{
    public static class HealthChecksMapping
    {
        public static WebApplication AddHealthChecks(this WebApplication app, string rateLimiterPolicyName)
        {
            app.MapHealthChecks("/health-check").AllowAnonymous().RequireRateLimiting(rateLimiterPolicyName);
            app.AddLiveness().RequireRateLimiting(rateLimiterPolicyName);
            app.AddReadiness().RequireRateLimiting(rateLimiterPolicyName);
            app.AddHealthDetails().RequireRateLimiting(rateLimiterPolicyName);
            return app;
        }
        private static IEndpointConventionBuilder AddHealthDetails(this WebApplication app)
        {
            return app.MapHealthChecks("/health-details", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).AllowAnonymous();
        }
        public static IEndpointConventionBuilder AddLiveness(this WebApplication app)
        {
            return app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = (check) => check.Name == "Liveness"
            });
        }
        public static IEndpointConventionBuilder AddReadiness(this WebApplication app)
        {
            return app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("ready")
            });
        }
    }
}
