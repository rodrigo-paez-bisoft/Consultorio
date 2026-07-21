using Bisoft.Consultorio.Api.Endpoints.Doctores;
namespace Bisoft.Consultorio.Api.Endpoints.Security
{
    public static class SecurityEndpointsGroup
    {
        public static RouteGroupBuilder MapSecurityEndpoints(this RouteGroupBuilder group)
        {
            var endpointGroup = group.MapGroup("auth").WithTags("Seguridad");
            endpointGroup.MapEndpoints();
            return endpointGroup;
        }
        public static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapLogin();
            return group;
        }
    }
}
