using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    public class UsuarioSqliteConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios")
                .HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(u => u.Nombre)
                .HasColumnName("nombre")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Username)
                .HasColumnName("username")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Activo)
                .HasColumnName("activo")
                .HasColumnType("INTEGER")
                .IsRequired()
                .HasDefaultValue(true);

            // Índices
            builder.HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("IX_Usuarios_Username");

            builder.HasIndex(u => u.Activo)
                .HasDatabaseName("IX_Usuarios_Activo");
        }
    }
}