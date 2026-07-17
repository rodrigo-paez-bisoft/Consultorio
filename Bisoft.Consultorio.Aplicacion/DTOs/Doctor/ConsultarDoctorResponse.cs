using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Doctor
{
    public class ConsultarDoctorResponse
    {
        public string Nombre { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Especialidad { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
