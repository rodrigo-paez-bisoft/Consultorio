using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class EliminarCita
    {
        private const string ENDPONT_NAME = "EliminarCita";
        public static RouteGroupBuilder MapEliminarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{citaId}",
                    async (
                        [FromRoute] Guid citaId,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        await citaService.EliminarCita(citaId);
                        return Results.Ok(new { message = "Cita eliminada correctamente." });
                    }
                )
                    .Produces(StatusCodes.Status200OK)
                    .WithDescription("Elimina una cita existente en el sistema.")
                    .WithSummary(ENDPONT_NAME)
                    .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
