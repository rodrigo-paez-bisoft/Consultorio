using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SeguridadContext _context;  // ← Cambiado a SeguridadContext

        public UsuarioRepository(SeguridadContext context)  // ← Cambiado a SeguridadContext
        {
            _context = context;
        }

        public IQueryable<Usuario> ConsultarUsuarios()
        {
            return _context.Usuarios.AsQueryable();
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<Usuario?> ObtenerUsuario(Guid usuarioId)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
        }

        public async Task<Usuario?> ObtenerUsuarioPorUsername(string username)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task RegistrarUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task ActualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public async Task EliminarUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }
    }
}