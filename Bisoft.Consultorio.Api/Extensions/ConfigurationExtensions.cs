using Bisoft.Consultorio.Api.DTOs.Configurations;

namespace Bisoft.Consultorio.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static GeneralConfigurations GetGeneralConfigurations(this IConfiguration configuration)
        {
            var connectionStringConsultorio = configuration.GetConnectionString("Consultorio");
            var connectionStringSeguridad = configuration.GetConnectionString("Seguridad");
            var rateLimiting = configuration.GetValue<int>("RateLimiting");
            var jwtConfig = configuration.GetJwtConfiguration();

            return new GeneralConfigurations(
                connectionStringConsultorio,
                connectionStringSeguridad,
                rateLimiting,
                jwtConfig);
        }

        private static string GetConnectionString(this IConfiguration configuration, string connectionName)
        {
            return configuration[$"DatabaseConnections:{connectionName}:ConnectionString"];
        }

        private static JwtConfiguration GetJwtConfiguration(this IConfiguration configuration)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var secreteKey = configuration["Jwt:SecretKey"];
            return new JwtConfiguration(audience, issuer, secreteKey);
        }
    }
}