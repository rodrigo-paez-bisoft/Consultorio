namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class PacientesEndpointGroup
    {
        public static RouteGroupBuilder MapPacientesEndpoints(this RouteGroupBuilder group)
        {
            //with tags signica que generara la documentacion de swagger con el nombre Pacientes
            var pacientesGroup = group.MapGroup("pacientes").WithTags("Pacientes");
            return pacientesGroup.MapEndpoints();
            
        }
        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarPacientesEndpoint();
            group.MapRegistrarPacienteEndponint();
            group.MapActualizarPacienteEndponint();
            group.MapEliminarPacienteEndponint();
            return group;
        }
    }
}
