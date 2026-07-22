using System;

namespace Bisoft.Consultorio.Api.DTOs.Usuario
{
    public class CambiarPasswordRequest
    {
        public string NuevaPassword { get; set; } = string.Empty;
    }
}