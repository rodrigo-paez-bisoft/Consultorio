using Bisoft.Consultorio.Aplicacion.DTOs.Paciente;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class ConsultarPaciente
    {
        private const string ENDPONT_NAME = "ConsultarPaciente";
        public static RouteGroupBuilder MapConsultarPacientesEndpoint(this RouteGroupBuilder group) 
        {
            group.MapGet("{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        PacienteService pacienteService,
                        CancellationToken ct
                    ) =>
                    {
                        
                            var paciente = await pacienteService.ConsultarPaciente(pacienteId);
                            return Results.Ok(paciente);
                       
                    }
                )
                    .Produces<ConsultarPacienteResponse>(StatusCodes.Status200OK)
                    .WithSummary("Consultar Paciente")
                    .WithName("Consultar Paciente");

            return group;
        }
    }
}
