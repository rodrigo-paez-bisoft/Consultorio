using System;

namespace Bisoft.Consultorio.Api.DTOs.Cita
{
    public class ReagendarCitaRequest
    {
        public DateTime NuevaFechaHora { get; set; }
        public int NuevaDuracionMinutos { get; set; }
        public Guid? SalaId { get; set; }
    }
}