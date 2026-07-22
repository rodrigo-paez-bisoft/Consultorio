using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class EliminarCita
    {
        private const string ENDPOINT_NAME = "EliminarCita";

        public static RouteGroupBuilder MapEliminarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{citaId}",
                    async (
                        [FromRoute] Guid citaId,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.EliminarCita(citaId);
                        return Results.Ok(new
                        {
                            message = "Cita eliminada correctamente.",
                            citaId = cita.Id,
                            fecha = cita.FechaHora,
                            motivo = cita.Motivo
                        });
                    }
                )
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithDescription("Elimina una cita existente en el sistema.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}