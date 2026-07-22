using Bisoft.Consultorio.Api.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class RegistrarCita
    {
        private const string ENDPOINT_NAME = "RegistrarCita";

        public static RouteGroupBuilder MapRegistrarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/api/citas",
                    async (
                        [FromBody] RegistrarCitaRequest request,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.RegistrarCita(
                            request.PacienteId,
                            request.DoctorId,
                            request.FechaHora,
                            request.DuracionMinutos,
                            request.Motivo,
                            request.SalaId,
                            request.Notas);

                        return Results.Created($"/api/citas/{cita.Id}", cita);
                    }
                )
                .Produces<RegistrarCitaResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status409Conflict)
                .WithDescription("Registra una nueva cita en el sistema.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}