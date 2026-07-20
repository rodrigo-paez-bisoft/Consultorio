using System;
using System.Collections.Generic;
using System.Text;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;
namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class SalaRepository : ISalaRepository
    {
        private readonly ConsultorioContext _context;
        public SalaRepository(ConsultorioContext context)
        {
            _context = context;
        }

        public IQueryable<Sala> ConsultaSalas()
        {
            return _context.Salas;
        }
        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }
        public async Task<Sala?> ObtenerSala(Guid salaId)
        {
            return await _context.Salas.OrderBy(p => p.Id).FirstOrDefaultAsync();
        }
        public async Task RegistrarSala(Sala sala)
        {
            await _context.Salas.AddAsync(sala);
        }
        public async Task EliminarSalas(Sala sala)
        {
            _context.Salas.Remove(sala);
        }
    }
}
