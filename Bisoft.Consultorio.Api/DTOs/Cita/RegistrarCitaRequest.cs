namespace Bisoft.Consultorio.Api.DTOs.Cita
{
    public class RegistrarCitaRequest
    {
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public int Status { get; set; }
        public Guid PacienteId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid SalaId { get; set; }
        public string Sala { get; set; } = string.Empty;
    }
}
