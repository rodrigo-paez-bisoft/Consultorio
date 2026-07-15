using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Paciente
{
    public class EliminarPacienteResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
