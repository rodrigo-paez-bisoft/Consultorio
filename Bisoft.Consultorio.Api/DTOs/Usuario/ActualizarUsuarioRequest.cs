using System;

namespace Bisoft.Consultorio.Api.DTOs.Usuario
{
    public class ActualizarUsuarioRequest
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}