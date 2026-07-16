namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class DoctoresEndpointGroup
    {
        public static RouteGroupBuilder MapDoctoresEndpoints(this RouteGroupBuilder group)
        {
            //with tags signica que generara la documentacion de swagger con el nombre Doctores
            var doctoresGroup = group.MapGroup("doctores").WithTags("Doctores");
            return doctoresGroup.MapEndpoints();
            
        }
        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarDoctoresEndpoint();
            group.MapRegistrarDoctorEndponint();
            group.MapActualizarDoctorEndponint();
            group.MapEliminarDoctorEndponint();
            return group;
        }
    }
}
