using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    public class DoctorSqliteConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctores")
                .HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .HasColumnName("Id")
                .HasColumnType("TEXT")
                .IsRequired();
            builder.Property(d => d.Nombre)
                .HasColumnName("nombre")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(d => d.Especialidad)
                .HasColumnName("especialidad")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
