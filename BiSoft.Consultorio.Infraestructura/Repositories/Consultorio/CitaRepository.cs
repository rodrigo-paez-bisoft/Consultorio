using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class CitaRepository : ICitaRepository
    {
        private readonly ConsultorioContext _context;

        public CitaRepository(ConsultorioContext context)
        {
            _context = context;
        }

        public IQueryable<Cita> ConsultarCitas()
        {
            return _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Include(c => c.Sala);
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<Cita?> ObtenerCita(Guid citaId)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Include(c => c.Sala)
                .FirstOrDefaultAsync(c => c.Id == citaId);
        }

        public async Task RegistrarCita(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
        }

        public async Task ActualizarCita(Cita cita)
        {
            _context.Citas.Update(cita);
        }

        public async Task EliminarCita(Cita cita)
        {
            _context.Citas.Remove(cita);
        }
    }
}