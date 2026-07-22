using Bisoft.Consultorio.Aplicacion.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class ConfirmarCita
    {
        private const string ENDPOINT_NAME = "ConfirmarCita";

        public static RouteGroupBuilder MapConfirmarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{citaId}/confirmar",
                    async (
                        [FromRoute] Guid citaId,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.ConfirmarCita(citaId);
                        return Results.Ok(new
                        {
                            message = "Cita confirmada correctamente.",
                            citaId = cita.Id,
                            fecha = cita.FechaHora,
                            status = cita.Status
                        });
                    }
                )
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithDescription("Confirma una cita existente.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}