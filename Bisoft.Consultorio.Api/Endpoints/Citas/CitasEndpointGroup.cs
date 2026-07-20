namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class CitasEndpointGroup
    {
        public static RouteGroupBuilder MapCitasEndpoints(this RouteGroupBuilder group)
        {
            //with tags significa que generara la documentacion de swagger con el nombre Citas
            var citasGroup = group.MapGroup("citas").WithTags("Citas");
            return citasGroup.MapEndpoints();

        }
        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarCitasEndpoint();
            group.MapRegistrarCitaEndpoint();
            group.MapActualizarCitaEndpoint();
            group.MapEliminarCitaEndpoint();
            return group;
        }
    }
}
