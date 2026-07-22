using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface ILoginAuditRepository
    {
        Task RegistrarLogin(LoginAudit loginAudit);
        Task GuardarCambios();
        Task<int> ContarIntentosFallidos(string usuario, DateTime desde);
    }
}