using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Infraestructura.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Repositories.Consultorio
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ConsultorioContext _context;
        public DoctorRepository(ConsultorioContext context)
        {
            _context = context;
        }
        public IQueryable<Doctor> ConsutarDoctores()
        {
            return _context.Doctores;
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<Doctor?> ObtenerDoctor(Guid doctorId)
        {
            return await _context.Doctores.OrderBy(d => d.Id).FirstOrDefaultAsync(d => d.Id == doctorId); 
        }

        public async Task RegistrarDoctor(Doctor doctor)
        {
            await _context.Doctores.AddAsync(doctor);
        }
    }
}
