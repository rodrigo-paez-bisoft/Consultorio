using System;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class LoginAudit
    {
        public Guid Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public bool Exitoso { get; set; }
        public DateTime FechaHora { get; set; }
        public string? IpAddress { get; set; }
    }
}