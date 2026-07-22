using System;
namespace Bisoft.Consultorio.Api.DTOs.Cita
{
    public class RegistrarCitaRequest
    {
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string? Notas { get; set; }
        public Guid PacienteId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid? SalaId { get; set; }
    }
}