using System;
using System.Collections.Generic;
using System.Text;
namespace Bisoft.Consultorio.Aplicacion.DTOs.Cita
{
    public static class EstadoCitaHelper
    {
        public static string GetNombreEstado(int status)
        {
            return status switch
            {
                1 => "Pendiente",
                2 => "Confirmada",
                3 => "Completada",
                4 => "Cancelada",
                5 => "No Asistió",
                _ => "Desconocido"
            };
        }
    }
}