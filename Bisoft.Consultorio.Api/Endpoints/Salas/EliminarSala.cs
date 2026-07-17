using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class EliminarSala
    {
        private const string ENDPONT_NAME = "EliminarSala";
        public static RouteGroupBuilder MapEliminarSalaEndponint(this RouteGroupBuilder group)
        {
            group.MapDelete("{salaId}",
                    async (
                        [FromRoute] Guid salaId,
                        SalaService SalaService,
                        CancellationToken ct
                    ) =>
                    {
                        await SalaService.EliminarSala(salaId);
                        return Results.Ok(new { message = "Sala eliminada correctamente." });
                    }
                )
                    .Produces(StatusCodes.Status200OK)
                    .WithDescription("Elimina una sala existente por su ID.")
                    .WithSummary(ENDPONT_NAME)
                    .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
