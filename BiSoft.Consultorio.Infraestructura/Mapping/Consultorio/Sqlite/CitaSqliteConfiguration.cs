using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    public class CitaSqliteConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Citas")
                .HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(c => c.Fecha)
                .HasColumnName("fecha")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(c => c.Motivo)
                .HasColumnName("motivo")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Status)
                .HasColumnName("status")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder.Property(c => c.Sala)
                .HasColumnName("sala")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
