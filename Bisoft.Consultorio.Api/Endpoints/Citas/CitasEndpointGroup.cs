namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class CitasEndpointGroup
    {
        public static RouteGroupBuilder MapCitasEndpoints(this RouteGroupBuilder group)
        {
            var citasGroup = group.MapGroup("citas").WithTags("Citas");
            return citasGroup.MapEndpoints();
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarCitasEndpoint();
            group.MapRegistrarCitaEndpoint();
            group.MapActualizarCitaEndpoint();
            group.MapEliminarCitaEndpoint();
            group.MapConfirmarCitaEndpoint();    // NUEVO
            group.MapCancelarCitaEndpoint();     // NUEVO
            group.MapReagendarCitaEndpoint();    // NUEVO
            return group;
        }
    }
}