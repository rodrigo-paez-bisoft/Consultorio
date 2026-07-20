using Bisoft.Consultorio.Api.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class ActualizarCita
    {
        private const string ENDPONT_NAME = "ActualizarCita";
        public static RouteGroupBuilder MapActualizarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{citaId}",
                    async (
                        [FromRoute] Guid citaId,
                        [FromBody] ActualizarCitaRequest request,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.ActualizarCita(citaId, request.Fecha, request.Motivo, request.Status, request.SalaId, request.Sala);
                        return Results.Ok(cita);
                    }
                )
                    .Produces<ActualizarCitaResponse>(StatusCodes.Status200OK)
                    .WithDescription("Actualiza una cita existente en el sistema.")
                    .WithSummary(ENDPONT_NAME)
                    .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
