using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.DTOs.Sala
{
    public class EliminarSalaResponse
    {
        public string Nombre { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
}
