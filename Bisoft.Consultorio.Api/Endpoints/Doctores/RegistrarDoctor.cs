using Bisoft.Consultorio.Api.DTOs.Doctor;
using Bisoft.Consultorio.Aplicacion.DTOs.Doctor;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class RegistrarDoctor
    {
        private const string ENDPONT_NAME = "RegistrarDoctor";
        public static RouteGroupBuilder MapRegistrarDoctorEndponint(this RouteGroupBuilder group)
        {
            group.MapPost("/api/doctores",
                    async (
                        [FromBody] RegistrarDoctorRequest request,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        var doctor = await doctorService.RegistrarDoctor(request.Nombre, request.Especialidad);
                        return Results.Ok(doctor);
                    }
                )
                .Produces<RegistrarDoctorResponse>(StatusCodes.Status201Created)
                .WithDescription("Registra un nuevo doctor en el sistema.")
                .WithSummary(ENDPONT_NAME)
                .WithName(ENDPONT_NAME);
            return group;
        }
    }
}
