using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Cita
{
    public class RegistrarCitaResponse
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public int Status { get; set; }
        public Guid PacienteId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid SalaId { get; set; }
        public string Sala { get; set; } = string.Empty;
    }
}
