// App/Endpoints/Security/Login.cs
using Bisoft.Consultorio.Api.DTOs.Configurations;
using Bisoft.Consultorio.Api.DTOs.Security;
using Bisoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bisoft.Consultorio.Api.Endpoints.Security;

public static class Login
{
    private const string ENDPOINT_NAME = "Login";

    public static RouteGroupBuilder MapLogin(this RouteGroupBuilder group)
    {
        group.MapPost("/", [AllowAnonymous]  // ← Cambiar de "/login" a "/"
        async (
                JwtConfiguration jwtConfiguration,
                [FromBody] LoginRequest request,
                UsuarioService usuarioService,
                LoginAuditService loginAuditService,
                HttpContext httpContext,
                CancellationToken ct
            ) =>
        {
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();

            // Autenticar usuario
            var usuario = await usuarioService.Autenticar(request.Username, request.Password);

            if (usuario == null)
            {
                await loginAuditService.RegistrarLogin(request.Username, false, ipAddress);
                return Results.Unauthorized();
            }

            // Generar token JWT
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secretkey));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.GivenName, usuario.Nombre)
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtConfiguration.Issuer,
                audience: jwtConfiguration.Audience,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials,
                claims: claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            // Registrar login exitoso
            await loginAuditService.RegistrarLogin(request.Username, true, ipAddress);

            return Results.Ok(new LoginResponse { Token = token });
        })
        .WithName("LoginEndpoint")  // ← Nombre único para evitar duplicados
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithDescription("Permite iniciar sesion")
        .WithSummary(ENDPOINT_NAME);

        return group;
    }
}