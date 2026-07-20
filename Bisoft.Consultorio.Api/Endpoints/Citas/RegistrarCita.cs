using Bisoft.Consultorio.Api.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.DTOs.Cita;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class RegistrarCita
    {
        private const string ENDPONT_NAME = "RegistrarCita";
        public static RouteGroupBuilder MapRegistrarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/api/citas",
                    async (
                        [FromBody] RegistrarCitaRequest request,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.RegistrarCita(request.Fecha, request.Motivo, request.Status, request.PacienteId, request.DoctorId, request.SalaId, request.Sala);
                        return Results.Ok(cita);
                    }
                )
                .Produces<RegistrarCitaResponse>(StatusCodes.Status201Created)
                .WithDescription("Registra una nueva cita en el sistema.")
                .WithSummary(ENDPONT_NAME)
                .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
