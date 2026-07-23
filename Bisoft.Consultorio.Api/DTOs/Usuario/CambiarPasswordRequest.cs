using System;

namespace Bisoft.Consultorio.Api.DTOs.Usuario
{
    public class CambiarPasswordRequest
    {
        public Guid UsuarioId { get; set; }  // ← Agregar UsuarioId
        public string PasswordActual { get; set; } = string.Empty;  // ← Agregar PasswordActual
        public string NuevaPassword { get; set; } = string.Empty;
    }
}