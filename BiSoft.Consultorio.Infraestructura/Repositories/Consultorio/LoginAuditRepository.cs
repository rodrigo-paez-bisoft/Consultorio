using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class LoginAuditRepository : ILoginAuditRepository
    {
        private readonly ConsultorioContext _context;

        public LoginAuditRepository(ConsultorioContext context)
        {
            _context = context;
        }

        public async Task RegistrarLogin(LoginAudit loginAudit)
        {
            await _context.LoginAudits.AddAsync(loginAudit);
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<int> ContarIntentosFallidos(string usuario, DateTime desde)
        {
            return await _context.LoginAudits
                .CountAsync(l => l.Usuario == usuario && !l.Exitoso && l.FechaHora >= desde);
        }
    }
}