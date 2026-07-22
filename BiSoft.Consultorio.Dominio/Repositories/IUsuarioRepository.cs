using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task RegistrarUsuario(Usuario usuario);
        Task ActualizarUsuario(Usuario usuario);
        Task GuardarCambios();
        Task<Usuario?> ObtenerUsuario(Guid usuarioId);
        Task<Usuario?> ObtenerUsuarioPorUsername(string username);
        IQueryable<Usuario> ConsultarUsuarios();
        Task EliminarUsuario(Usuario usuario);
    }
}