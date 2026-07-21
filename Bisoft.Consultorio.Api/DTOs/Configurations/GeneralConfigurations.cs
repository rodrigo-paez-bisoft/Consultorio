namespace Bisoft.Consultorio.Api.DTOs.Configurations;

public record GeneralConfigurations
    (
       string ConnectionString,
        int RateLimit,
        JwtConfiguration JWT
    );