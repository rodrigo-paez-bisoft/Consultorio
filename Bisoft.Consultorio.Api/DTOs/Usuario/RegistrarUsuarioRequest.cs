using System;

namespace Bisoft.Consultorio.Api.DTOs.Usuario
{
    public class RegistrarUsuarioRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}