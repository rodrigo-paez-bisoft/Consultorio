using Bisoft.Consultorio.Api.DTOs.Paciente;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class ActualizarPaciente
    {
        private const string ENDPONT_NAME = "ActualizarPaciente";
        public static RouteGroupBuilder MapActualizarPacienteEndponint(this RouteGroupBuilder group) {
            group.MapPut("{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        [FromBody] ActualizarPacienteRequest request,
                        PacienteService pacienteService,
                        CancellationToken ct
                    ) =>
                    {
                        var paciente = await pacienteService.RegistrarPaciente(request.Nombre);
                        return Results.Ok(paciente);
                    }
                )
                    .Produces<RegistrarPacienteRequest>(StatusCodes.Status200OK)
                    .WithDescription("Actualiza un nuevo paciente en el sistema.")
                    .WithSummary(ENDPONT_NAME)
                    .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
