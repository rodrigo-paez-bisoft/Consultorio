using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class EliminarDoctor
    {
        private const string ENDPONT_NAME = "EliminarDoctor";
        public static RouteGroupBuilder MapEliminarDoctorEndponint(this RouteGroupBuilder group)
        {
            group.MapDelete("{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        await doctorService.EliminarDoctor(doctorId);
                        return Results.Ok(new { message = "Doctor eliminado correctamente." });
                    }
                )
                    .Produces(StatusCodes.Status200OK)
                    .WithDescription("Elimina un doctor existente en el sistema.")
                    .WithSummary(ENDPONT_NAME)
                    .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
