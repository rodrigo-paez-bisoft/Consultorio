namespace Bisoft.Consultorio.Api.DTOs.Configurations
{
    public record GeneralConfigurations
    (
        string ConnectionStringConsultorio,
        string ConnectionStringSeguridad,
        int RateLimit,
        JwtConfiguration JWT
    );
}