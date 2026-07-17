using Bisoft.Consultorio.Api.Endpoints.Pacientes;

namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class SalasEndpointGroup
    {
        public static RouteGroupBuilder MapSalasEndpoints(this RouteGroupBuilder group)
        {
            //with tags signica que generara la documentacion de swagger con el nombre Pacientes
            var salasGroup = group.MapGroup("salas").WithTags("Salas");
            return salasGroup.MapEndpoints();

        }
        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarSalasEndpoint();
            group.MapRegistrarSalaEndponint();
            group.MapActualizarSalaEndpoint();
            group.MapEliminarSalaEndponint();
            return group;
        }
    }
}
