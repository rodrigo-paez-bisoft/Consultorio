using Bisoft.Consultorio.Aplicacion.DTOs.Doctor;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class ConsultarDoctor
    {
        public static RouteGroupBuilder MapConsultarDoctoresEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        
                            var doctor = await doctorService.ConsultarDoctor(doctorId);
                            return Results.Ok(doctor);

                    }
                )
                .Produces<ConsultarDoctorResponse>(StatusCodes.Status200OK)
                .WithDescription("Consulta un doctor por su ID.")
                .WithSummary("Consultar Doctor")
                .WithName("Consultar Doctor");
            return group;
        }
    }
}
