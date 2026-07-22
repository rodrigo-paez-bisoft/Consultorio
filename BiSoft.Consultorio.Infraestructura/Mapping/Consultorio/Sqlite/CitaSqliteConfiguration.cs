using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    public class CitaSqliteConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            // Tabla y clave primaria
            builder.ToTable("Citas")
                .HasKey(c => c.Id);

            // ===== PROPIEDADES =====
            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(c => c.FechaHora)
                .HasColumnName("fecha_hora")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(c => c.DuracionMinutos)
                .HasColumnName("duracion_minutos")
                .HasColumnType("INTEGER")
                .IsRequired()
                .HasDefaultValue(30);

            builder.Property(c => c.Motivo)
                .HasColumnName("motivo")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Notas)
                .HasColumnName("notas")
                .HasColumnType("TEXT")
                .HasMaxLength(1000);

            builder.Property(c => c.Status)
                .HasColumnName("status")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder.Property(c => c.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(c => c.FechaCancelacion)
                .HasColumnName("fecha_cancelacion")
                .HasColumnType("TEXT");

            builder.Property(c => c.MotivoCancelacion)
                .HasColumnName("motivo_cancelacion")
                .HasColumnType("TEXT")
                .HasMaxLength(500);

            // ===== FK - Paciente =====
            builder.Property(c => c.PacienteId)
                .HasColumnName("paciente_id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== FK - Doctor =====
            builder.Property(c => c.DoctorId)
                .HasColumnName("doctor_id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== FK - Sala (opcional) =====
            builder.Property(c => c.SalaId)
                .HasColumnName("sala_id")
                .HasColumnType("TEXT");

            builder.HasOne(c => c.Sala)
                .WithMany()
                .HasForeignKey(c => c.SalaId)
                .OnDelete(DeleteBehavior.SetNull);

            // ===== ÍNDICES =====
            builder.HasIndex(c => c.FechaHora)
                .HasDatabaseName("IX_Citas_FechaHora");

            builder.HasIndex(c => c.DoctorId)
                .HasDatabaseName("IX_Citas_DoctorId");

            builder.HasIndex(c => c.PacienteId)
                .HasDatabaseName("IX_Citas_PacienteId");

            builder.HasIndex(c => c.Status)
                .HasDatabaseName("IX_Citas_Status");

            builder.HasIndex(c => c.SalaId)
                .HasDatabaseName("IX_Citas_SalaId");
        }
    }
}