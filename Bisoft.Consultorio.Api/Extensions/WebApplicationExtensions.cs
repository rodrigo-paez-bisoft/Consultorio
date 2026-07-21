using Bisoft.Consultorio.Api.Endpoints.Citas;
using Bisoft.Consultorio.Api.Endpoints.Doctores;
using Bisoft.Consultorio.Api.Endpoints.Pacientes;
using Bisoft.Consultorio.Api.Endpoints.Salas;
using Bisoft.Consultorio.Api.Endpoints.Security;
using Microsoft.OpenApi;

namespace Bisoft.Consultorio.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            var apiEndpoints = app.MapGroup("api")
                                  .AddOpenApi()
                                  .RequireRateLimiting(Program.RATE_LIMITER_POLICY_NAME)
                                  .RequireAuthorization();
            apiEndpoints.MapDoctoresEndpoints();
            apiEndpoints.MapSecurityEndpoints();
            apiEndpoints.MapPacientesEndpoints();
            apiEndpoints.MapSalasEndpoints();
            apiEndpoints.MapCitasEndpoints();
            return app;
        }
        private static RouteGroupBuilder AddOpenApi(this RouteGroupBuilder group)
        {
            return group.AddOpenApiOperationTransformer((options, context, ct) =>
            {
                options.Responses ??= new OpenApiResponses();
                options.Responses["400"] = new OpenApiResponse { Description = "Solicitud incorrecta" };
                options.Responses["404"] = new OpenApiResponse { Description = "No encontrado" };
                options.Responses["500"] = new OpenApiResponse { Description = "Error interno del servidor" };
                return Task.CompletedTask;
            });
        }
    }
}
