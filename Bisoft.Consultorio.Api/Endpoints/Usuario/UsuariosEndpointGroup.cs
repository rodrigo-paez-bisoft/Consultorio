// App/Endpoints/Usuarios/UsuariosEndpointGroup.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Bisoft.Consultorio.Aplicacion.Services;
using Bisoft.Consultorio.Api.DTOs.Usuario;

namespace Bisoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class UsuariosEndpointGroup
    {
        // ✅ Método que recibe IEndpointRouteBuilder
        public static void MapUsuariosEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("api/usuarios")
                .WithTags("Usuarios")
                .RequireAuthorization();

            // Registrar usuario (público - sin autenticación)
            group.MapPost("/registrar", RegistrarUsuario)
                .AllowAnonymous();

            // Listar todos los usuarios
            group.MapGet("/", ConsultarTodos);

            // Listar usuarios activos
            group.MapGet("/activos", ConsultarActivos);

            // Obtener usuario por ID
            group.MapGet("/{id:guid}", ConsultarPorId);

            // Obtener usuario por username
            group.MapGet("/username/{username}", ConsultarPorUsername);

            // Actualizar usuario
            group.MapPut("/{id:guid}", ActualizarUsuario);

            // Cambiar contraseña
            group.MapPut("/{id:guid}/cambiar-password", CambiarPassword);

            // Eliminar usuario
            group.MapDelete("/{id:guid}", EliminarUsuario);
        }

        private static async Task<IResult> RegistrarUsuario(
            RegistrarUsuarioRequest request,
            UsuarioService service)
        {
            try
            {
                var usuario = await service.RegistrarUsuario(
                    request.Nombre,
                    request.Username,
                    request.Password
                );

                return Results.Created($"/api/usuarios/{usuario.Id}", new
                {
                    id = usuario.Id,
                    nombre = usuario.Nombre,
                    username = usuario.Username,
                    activo = usuario.Activo
                });
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Error al registrar usuario: {ex.Message}");
            }
        }

        private static async Task<IResult> ConsultarTodos(UsuarioService service)
        {
            var usuarios = await service.ConsultarUsuarios();

            var response = usuarios.Select(u => new
            {
                id = u.Id,
                nombre = u.Nombre,
                username = u.Username,
                activo = u.Activo
            });

            return Results.Ok(response);
        }

        private static async Task<IResult> ConsultarActivos(UsuarioService service)
        {
            var usuarios = await service.ConsultarUsuariosActivos();

            var response = usuarios.Select(u => new
            {
                id = u.Id,
                nombre = u.Nombre,
                username = u.Username,
                activo = u.Activo
            });

            return Results.Ok(response);
        }

        private static async Task<IResult> ConsultarPorId(Guid id, UsuarioService service)
        {
            try
            {
                var usuario = await service.ObtenerUsuario(id);

                return Results.Ok(new
                {
                    id = usuario.Id,
                    nombre = usuario.Nombre,
                    username = usuario.Username,
                    activo = usuario.Activo
                });
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound(new { mensaje = "Usuario no encontrado" });
            }
        }

        private static async Task<IResult> ConsultarPorUsername(string username, UsuarioService service)
        {
            var usuario = await service.ObtenerUsuarioPorUsername(username);

            if (usuario == null)
                return Results.NotFound(new { mensaje = "Usuario no encontrado" });

            return Results.Ok(new
            {
                id = usuario.Id,
                nombre = usuario.Nombre,
                username = usuario.Username,
                activo = usuario.Activo
            });
        }

        private static async Task<IResult> ActualizarUsuario(
            Guid id,
            ActualizarUsuarioRequest request,
            UsuarioService service)
        {
            try
            {
                var usuario = await service.ActualizarUsuario(
                    id,
                    request.Nombre,
                    request.Username,
                    request.Activo
                );

                return Results.Ok(new
                {
                    id = usuario.Id,
                    nombre = usuario.Nombre,
                    username = usuario.Username,
                    activo = usuario.Activo
                });
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound(new { mensaje = "Usuario no encontrado" });
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { mensaje = ex.Message });
            }
        }

        private static async Task<IResult> CambiarPassword(
            Guid id,
            CambiarPasswordRequest request,
            UsuarioService service)
        {
            try
            {
                var usuario = await service.CambiarPasswordConVerificacion(
                    id,
                    request.PasswordActual,
                    request.NuevaPassword
                );

                return Results.Ok(new { mensaje = "Contraseña actualizada exitosamente" });
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound(new { mensaje = "Usuario no encontrado" });
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { mensaje = ex.Message });
            }
        }

        private static async Task<IResult> EliminarUsuario(Guid id, UsuarioService service)
        {
            var resultado = await service.EliminarUsuario(id);

            if (!resultado)
                return Results.NotFound(new { mensaje = "Usuario no encontrado" });

            return Results.NoContent();
        }
    }
}