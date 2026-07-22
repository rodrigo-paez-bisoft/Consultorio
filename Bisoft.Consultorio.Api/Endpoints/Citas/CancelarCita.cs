using Bisoft.Consultorio.Api.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class CancelarCita
    {
        private const string ENDPOINT_NAME = "CancelarCita";

        public static RouteGroupBuilder MapCancelarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{citaId}/cancelar",
                    async (
                        [FromRoute] Guid citaId,
                        [FromBody] CancelarCitaRequest request,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.CancelarCita(citaId, request.MotivoCancelacion);

                        var mensaje = cita.Status == 4 ? "Cita cancelada correctamente." : "Cita marcada como No Asistió por cancelación tardía.";

                        return Results.Ok(new
                        {
                            message = mensaje,
                            citaId = cita.Id,
                            fecha = cita.FechaHora,
                            status = cita.Status,
                            motivoCancelacion = cita.MotivoCancelacion
                        });
                    }
                )
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithDescription("Cancela una cita existente.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}