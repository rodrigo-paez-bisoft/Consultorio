using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Paciente
{
    public class ConsultarPacienteResponse
    {
        public string Nombre { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
}
