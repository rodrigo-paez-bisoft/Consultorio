namespace Bisoft.Consultorio.Api.DTOs.Cita
{
    public class ActualizarCitaRequest
    {
        public DateTime? FechaHora { get; set; }
        public int? DuracionMinutos { get; set; }
        public string? Motivo { get; set; }
        public string? Notas { get; set; }
        public Guid? SalaId { get; set; }
        public string? MotivoCancelacion { get; set; }
    }
}
