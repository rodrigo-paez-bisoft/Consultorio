using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Doctor
{
    public class EliminarDoctorResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
