using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Helpers;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;  // ← IMPORTANTE
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

        public async Task<Usuario?> Autenticar(string username, string password)
        {
            Console.WriteLine($"=== AUTENTICACIÓN ===");
            Console.WriteLine($"Usuario: {username}");
            Console.WriteLine($"Password: {password}");

            var usuario = await _usuarioRepository.ObtenerUsuarioPorUsername(username);

            if (usuario == null)
            {
                Console.WriteLine("❌ Usuario NO encontrado");
                return null;
            }

            Console.WriteLine($"✅ Usuario encontrado: {usuario.Username}");
            Console.WriteLine($"Hash en BD: '{usuario.PasswordHash}'");

            var hashCalculado = PasswordHelper.HashPassword(password);
            Console.WriteLine($"Hash calculado: '{hashCalculado}'");
            Console.WriteLine($"Coinciden: {usuario.PasswordHash == hashCalculado}");

            if (!usuario.Activo)
            {
                Console.WriteLine("❌ Usuario INACTIVO");
                return null;
            }

            if (usuario.PasswordHash != hashCalculado)
            {
                Console.WriteLine("❌ Contraseña INCORRECTA");
                return null;
            }

            Console.WriteLine("✅ Autenticación exitosa");
            return usuario;
        }

        public async Task<Usuario> ObtenerUsuario(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(usuarioId)
                ?? throw new KeyNotFoundException($"Usuario con ID {usuarioId} no encontrado");
            return usuario;
        }

        // ✅ CORREGIDO: Con ToListAsync()
        public async Task<IEnumerable<Usuario>> ConsultarUsuarios()
        {
            return await _usuarioRepository.ConsultarUsuarios().ToListAsync();
        }

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
    }
}