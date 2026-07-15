using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Infraestructura.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Repositories.Consultorio
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ConsultorioContext _context;
        public PacienteRepository(ConsultorioContext context)
        {
            _context = context;
        }
        public IQueryable<Paciente> ConsultarPacientes()
        {
            return _context.Pacientes;
        }
        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }
        public async Task<Paciente?> ObtenerPaciente(Guid pacienteId)
        {
            return await _context.Pacientes.OrderBy(p => p.Id).FirstOrDefaultAsync(p => p.Id == pacienteId);
        }
        public async Task RegistrarPaciente(Paciente paciente)
        {
            await _context.Pacientes.AddAsync(paciente);
        }
    }

}