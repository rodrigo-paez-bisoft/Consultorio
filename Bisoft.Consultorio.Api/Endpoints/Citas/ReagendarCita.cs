using Bisoft.Consultorio.Api.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class ReagendarCita
    {
        private const string ENDPOINT_NAME = "ReagendarCita";

        public static RouteGroupBuilder MapReagendarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{citaId}/reagendar",
                    async (
                        [FromRoute] Guid citaId,
                        [FromBody] ReagendarCitaRequest request,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.ReagendarCita(
                            citaId,
                            request.NuevaFechaHora,
                            request.NuevaDuracionMinutos,
                            request.SalaId);

                        return Results.Ok(new
                        {
                            message = "Cita reagendada correctamente.",
                            citaId = cita.Id,
                            nuevaFecha = cita.FechaHora,
                            nuevaDuracion = cita.DuracionMinutos,
                            status = cita.Status
                        });
                    }
                )
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status409Conflict)
                .WithDescription("Reagenda una cita existente.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}