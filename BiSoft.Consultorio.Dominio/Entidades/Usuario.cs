using System;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
            Activo = true;
        }

        public Usuario(string nombre, string username, string passwordHash)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Username = username;
            PasswordHash = passwordHash;
            Activo = true;
        }

        public void Actualizar(string nombre, string username, bool activo)
        {
            Nombre = nombre;
            Username = username;
            Activo = activo;
        }

        public void CambiarPassword(string nuevoPasswordHash)
        {
            PasswordHash = nuevoPasswordHash;
        }
    }
}