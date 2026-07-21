using Bisoft.Consultorio.Api.DTOs.Configurations;
using Bisoft.Consultorio.Api.DTOs.Security;
using Bisoft.Consultorio.Aplicacion.DTOs.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bisoft.Consultorio.Api.Endpoints.Security;
public static class Login
{
    private const string ENDPOINT_NAME = "Login";
    private const string USUARIO = "admin";
    private const string PASSWORD = "password"; 
    public static RouteGroupBuilder MapLogin(this RouteGroupBuilder group)
    {
        group.MapPost("Login", [AllowAnonymous]
            async (
                JwtConfiguration jwtConfiguration,
                [FromBody] LoginRequest request,
                CancellationToken ct
            ) =>
            {
                if (request.Usuario != USUARIO || request.Password != PASSWORD)
                    return Results.Unauthorized();
                
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secretkey) );
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var TokenOptions = new JwtSecurityToken(
                    issuer: jwtConfiguration.Issuer,
                    audience: jwtConfiguration.Audience,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials,
                    claims: new List<Claim>()
                );
                var token = new JwtSecurityTokenHandler().WriteToken(TokenOptions);
                return Results.Ok(new LoginResponse { Token=token });

            }
        )
        .Produces<ConsultarDoctorResponse>(StatusCodes.Status200OK)
        .WithDescription("Permite iniciar sesion")
        .WithSummary(ENDPOINT_NAME)
        .WithName(ENDPOINT_NAME);
        return group;
    }
}

