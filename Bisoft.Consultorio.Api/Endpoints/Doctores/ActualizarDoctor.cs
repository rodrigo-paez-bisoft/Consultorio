using Bisoft.Consultorio.Api.DTOs.Doctor;
using Bisoft.Consultorio.Aplicacion.DTOs.Doctor;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class ActualizarDoctor
    {
        private const string ENDPONT_NAME = "ActualizarDoctor";
        public static RouteGroupBuilder MapActualizarDoctorEndponint(this RouteGroupBuilder group)
        {
            group.MapPut("{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        [FromBody] ActualizarDoctorRequest request,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        var doctor = await doctorService.ActualizarDoctor(doctorId, request.Nombre, request.Especialidad);
                        return Results.Ok(doctor);
                    }
                )
                    .Produces<RegistrarDoctorResponse>(StatusCodes.Status200OK)
                    .WithDescription("Actualiza un nuevo doctor en el sistema.")
                    .WithSummary(ENDPONT_NAME)
                    .WithName(ENDPONT_NAME);
            return group;
        }
    }

}
