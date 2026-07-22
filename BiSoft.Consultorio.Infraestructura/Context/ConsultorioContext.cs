using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Infraestructura.Context
{
    public class ConsultorioContext : DbContext
    {
        public ConsultorioContext(DbContextOptions<ConsultorioContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Cita> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new SalaSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new CitaSqliteConfiguration());
        }
    }
}