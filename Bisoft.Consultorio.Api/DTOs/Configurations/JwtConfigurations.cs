namespace Bisoft.Consultorio.Api.DTOs.Configurations
{
    public record JwtConfiguration(string Audience, string Issuer, string Secretkey);
}
