using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BiSoft.Consultorio.Dominio.Entidades;
using Infraestructura.Mapping.Consultorio.Sqlite;

namespace Infraestructura.Context
{
    public class ConsultorioContext : DbContext
    {
        public DbSet<Doctor>Doctores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public ConsultorioContext(DbContextOptions<ConsultorioContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DoctorSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteSqliteConfiguration());
        }
    }
}
