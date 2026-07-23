// App/Endpoints/Security/SecurityEndpointsGroup.cs
using Bisoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bisoft.Consultorio.Api.Endpoints.Security
{
    public static class SecurityEndpointsGroup
    {
        // ✅ Método que recibe IEndpointRouteBuilder
        public static void MapSecurityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("auth")
                .WithTags("Seguridad")
                .AllowAnonymous();

            // Usar el Login que ya tienes
            group.MapLogin();
        }

        private static async Task<IResult> Login(
            LoginRequest request,
            UsuarioService service,
            IConfiguration configuration)
        {
            try
            {
                // Autenticar usuario
                var usuario = await service.Autenticar(request.Username, request.Password);

                if (usuario == null)
                {
                    return Results.Unauthorized();
                }

                // Generar token
                var token = GenerarTokenJWT(usuario, configuration);

                return Results.Ok(new
                {
                    token = token,
                    usuarioId = usuario.Id,
                    nombre = usuario.Nombre,
                    username = usuario.Username,
                    rol = "Usuario"
                });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Error al autenticar: {ex.Message}");
            }
        }

        private static string GenerarTokenJWT(Usuario usuario, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtConfig["SecretKey"] ?? "mi-clave-secreta-por-defecto-1234567890");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.GivenName, usuario.Nombre),
                new Claim(ClaimTypes.Role, "Usuario")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = jwtConfig["Issuer"] ?? "Bisoft.Consultorio",
                Audience = jwtConfig["Audience"] ?? "Bisoft.Consultorio.Users",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}