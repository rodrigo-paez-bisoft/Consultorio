using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Paciente
{
    public class ActualizarPacienteResponse
    {
        public string Nombre { get; set; } = string.Empty;
        public Guid Id { get; set; }
    }
}
