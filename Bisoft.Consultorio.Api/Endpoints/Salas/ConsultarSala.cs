using Bisoft.Consultorio.Aplicacion.DTOs.Paciente;
using Bisoft.Consultorio.Aplicacion.DTOs.Sala;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class ConsultarSala
    {
        private const string ENDPONT_NAME = "ConsultarSala";
        public static RouteGroupBuilder MapConsultarSalasEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{salaId}",
                    async (
                        [FromRoute] Guid salaId,
                        SalaService salaService,
                        CancellationToken ct
                    ) =>
                    {
                        var sala = await salaService.ConsultarSala(salaId);
                        return Results.Ok(sala);
                    }
                )
                    .Produces<ConsultaSalaResponse>(StatusCodes.Status200OK)
                    .WithSummary(ENDPONT_NAME)
                    .WithName(ENDPONT_NAME);

            return group;
        }
    }
}
