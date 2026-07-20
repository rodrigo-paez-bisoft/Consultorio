using Bisoft.Consultorio.Aplicacion.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class ConsultarCita
    {
        public static RouteGroupBuilder MapConsultarCitasEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{citaId}",
                    async (
                        [FromRoute] Guid citaId,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.ConsultarCita(citaId);
                        return Results.Ok(cita);
                    }
                )
                .Produces<ConsultarCitaResponse>(StatusCodes.Status200OK)
                .WithDescription("Consulta una cita por su ID.")
                .WithSummary("Consultar Cita")
                .WithName("Consultar Cita");
            return group;
        }
    }
}
