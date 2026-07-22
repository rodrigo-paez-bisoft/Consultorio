using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    public class LoginAuditSqliteConfiguration : IEntityTypeConfiguration<LoginAudit>
    {
        public void Configure(EntityTypeBuilder<LoginAudit> builder)
        {
            builder.ToTable("LoginAudits")
                .HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasColumnName("Id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(l => l.Usuario)
                .HasColumnName("usuario")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Exitoso)
                .HasColumnName("exitoso")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder.Property(l => l.FechaHora)
                .HasColumnName("fecha_hora")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(l => l.IpAddress)
                .HasColumnName("ip_address")
                .HasColumnType("TEXT")
                .HasMaxLength(45);

            // Índices
            builder.HasIndex(l => l.Usuario)
                .HasDatabaseName("IX_LoginAudits_Usuario");

            builder.HasIndex(l => l.FechaHora)
                .HasDatabaseName("IX_LoginAudits_FechaHora");

            builder.HasIndex(l => new { l.Usuario, l.FechaHora })
                .HasDatabaseName("IX_LoginAudits_Usuario_FechaHora");
        }
    }
}