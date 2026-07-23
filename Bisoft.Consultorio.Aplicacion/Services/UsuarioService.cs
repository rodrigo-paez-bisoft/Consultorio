using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Helpers;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bisoft.Consultorio.Aplicacion.Services
{
    public class UsuarioService
    {
        private readonly ILogger<UsuarioService> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(ILogger<UsuarioService> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        // Registrar usuario
        public async Task<Usuario> RegistrarUsuario(string nombre, string username, string password)
        {
            var existente = await _usuarioRepository.ObtenerUsuarioPorUsername(username);
            if (existente != null)
                throw new InvalidOperationException($"El usuario '{username}' ya existe");

            var passwordHash = PasswordHelper.HashPassword(password);
            var usuario = new Usuario(nombre, username, passwordHash);

            await _usuarioRepository.RegistrarUsuario(usuario);
            await _usuarioRepository.GuardarCambios();

            _logger.LogInformation("Usuario registrado: {Username}", username);
            return usuario;
        }

        // Autenticar usuario
        public async Task<Usuario?> Autenticar(string username, string password)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorUsername(username);

            if (usuario == null)
            {
                _logger.LogWarning("Intento de autenticación fallido: usuario {Username} no encontrado", username);
                return null;
            }

            if (!usuario.Activo)
            {
                _logger.LogWarning("Intento de autenticación: usuario {Username} inactivo", username);
                return null;
            }

            var hashCalculado = PasswordHelper.HashPassword(password);
            if (usuario.PasswordHash != hashCalculado)
            {
                _logger.LogWarning("Intento de autenticación fallido: contraseña incorrecta para {Username}", username);
                return null;
            }

            _logger.LogInformation("Usuario autenticado exitosamente: {Username}", username);
            return usuario;
        }

        // Obtener usuario por ID
        public async Task<Usuario> ObtenerUsuario(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(usuarioId)
                ?? throw new KeyNotFoundException($"Usuario con ID {usuarioId} no encontrado");
            return usuario;
        }

        // Obtener usuario por username
        public async Task<Usuario?> ObtenerUsuarioPorUsername(string username)
        {
            return await _usuarioRepository.ObtenerUsuarioPorUsername(username);
        }

        // Consultar todos los usuarios
        public async Task<IEnumerable<Usuario>> ConsultarUsuarios()
        {
            return await _usuarioRepository.ConsultarUsuarios().ToListAsync();
        }

        // Consultar usuarios activos
        public async Task<IEnumerable<Usuario>> ConsultarUsuariosActivos()
        {
            return await _usuarioRepository.ConsultarUsuarios()
                .Where(u => u.Activo)
                .ToListAsync();
        }

        // Actualizar usuario
        public async Task<Usuario> ActualizarUsuario(Guid usuarioId, string nombre, string username, bool activo)
        {
            var usuario = await ObtenerUsuario(usuarioId);

            var existente = await _usuarioRepository.ObtenerUsuarioPorUsername(username);
            if (existente != null && existente.Id != usuarioId)
                throw new InvalidOperationException($"El usuario '{username}' ya existe");

            usuario.Actualizar(nombre, username, activo);

            await _usuarioRepository.ActualizarUsuario(usuario);
            await _usuarioRepository.GuardarCambios();

            _logger.LogInformation("Usuario actualizado: {Username}", username);
            return usuario;
        }

        // Cambiar contraseña
        public async Task<Usuario> CambiarPassword(Guid usuarioId, string nuevaPassword)
        {
            var usuario = await ObtenerUsuario(usuarioId);
            var passwordHash = PasswordHelper.HashPassword(nuevaPassword);
            usuario.CambiarPassword(passwordHash);

            await _usuarioRepository.ActualizarUsuario(usuario);
            await _usuarioRepository.GuardarCambios();

            _logger.LogInformation("Password cambiada para usuario: {Username}", usuario.Username);
            return usuario;
        }

        // Cambiar contraseña con verificación
        public async Task<Usuario> CambiarPasswordConVerificacion(Guid usuarioId, string passwordActual, string nuevaPassword)
        {
            var usuario = await ObtenerUsuario(usuarioId);

            // Verificar contraseña actual
            var hashActual = PasswordHelper.HashPassword(passwordActual);
            if (usuario.PasswordHash != hashActual)
                throw new InvalidOperationException("La contraseña actual es incorrecta");

            // Cambiar contraseña
            var nuevoHash = PasswordHelper.HashPassword(nuevaPassword);
            usuario.CambiarPassword(nuevoHash);

            await _usuarioRepository.ActualizarUsuario(usuario);
            await _usuarioRepository.GuardarCambios();

            _logger.LogInformation("Password cambiada para usuario: {Username}", usuario.Username);
            return usuario;
        }

        // Eliminar usuario (baja lógica)
        public async Task<bool> EliminarUsuario(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(usuarioId);
            if (usuario == null)
                return false;

            // Baja lógica: desactivar en lugar de eliminar
            usuario.Activo = false;
            await _usuarioRepository.ActualizarUsuario(usuario);
            await _usuarioRepository.GuardarCambios();

            _logger.LogInformation("Usuario desactivado: {Username}", usuario.Username);
            return true;
        }

        // Eliminar usuario (baja física)
        public async Task<bool> EliminarUsuarioFisico(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(usuarioId);
            if (usuario == null)
                return false;

            await _usuarioRepository.EliminarUsuario(usuario);
            await _usuarioRepository.GuardarCambios();

            _logger.LogInformation("Usuario eliminado físicamente: {Username}", usuario.Username);
            return true;
        }

        // Verificar existencia
        public async Task<bool> ExisteUsuario(string username)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorUsername(username);
            return usuario != null;
        }
    }
}