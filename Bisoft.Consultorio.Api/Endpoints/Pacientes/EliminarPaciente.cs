using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    
    public static class EliminarPaciente
    {
        private const string ENDPONT_NAME = "EliminarPaciente";
        public static RouteGroupBuilder MapEliminarPacienteEndponint(this RouteGroupBuilder group)
        {
            group.MapDelete("{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        PacienteService pacienteService,
                        CancellationToken ct
                    ) =>
                    {
                        await pacienteService.EliminarPaciente(pacienteId);
                        return Results.Ok(new { message = "Paciente eliminado correctamente." });
                    }
                )
                    .Produces(StatusCodes.Status200OK)
                    .WithDescription("Elimina un paciente existente por su ID.")
                    .WithSummary(ENDPONT_NAME)
                    .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
