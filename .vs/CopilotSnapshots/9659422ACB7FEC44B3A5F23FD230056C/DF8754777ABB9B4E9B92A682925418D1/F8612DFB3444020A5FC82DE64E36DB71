using Bisoft.Consultorio.Api.DTOs.Paciente;
using Bisoft.Consultorio.Api.DTOs.Sala;
using Bisoft.Consultorio.Aplicacion.DTOs.Paciente;
using Bisoft.Consultorio.Aplicacion.DTOs.Sala;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class RegistrarSala
    {
        private const string ENDPONT_NAME = "RegistrarSala";
        public static RouteGroupBuilder MapRegistrarSalaEndponint(this RouteGroupBuilder group)
        {
            group.MapPost("/api/Salas",
                    async (
                        [FromBody] RegistraSalaRequest request,
                        SalaService salaService,
                        CancellationToken ct
                    ) =>
                    {

                        var sala = await salaService.RegistrarSala(request.Nombre);
                        return Results.Ok(sala);

                    }
                )
                .Produces<RegistraSalaResponse>(StatusCodes.Status201Created)
                .WithDescription("Registra una nueva sala en el sistema.")
                .WithSummary(ENDPONT_NAME)
                .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
