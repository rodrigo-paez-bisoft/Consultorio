using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Infraestructura.Context
{
    public class SeguridadContext : DbContext
    {
        public SeguridadContext(DbContextOptions<SeguridadContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LoginAudit> LoginAudits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new LoginAuditSqliteConfiguration());
        }
    }
}