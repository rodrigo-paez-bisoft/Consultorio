using System;
using System.Collections.Generic;
using System.Text;
using System;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Cita
{
    public class ConfirmarCitaResponse
    {
        public Guid Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public int Status { get; set; }
        public Guid PacienteId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid? SalaId { get; set; }
    }
}