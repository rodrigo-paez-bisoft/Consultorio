using Bisoft.Consultorio.Api.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class ActualizarCita
    {
        private const string ENDPOINT_NAME = "ActualizarCita";

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
                        var cita = await citaService.ActualizarCita(
                            citaId,
                            request.FechaHora,
                            request.DuracionMinutos,
                            request.Motivo,
                            request.Notas,
                            request.SalaId,
                            request.MotivoCancelacion);

                        return Results.Ok(cita);
                    }
                )
                .Produces<ActualizarCitaResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status409Conflict)
                .WithDescription("Actualiza una cita existente en el sistema.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}