using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Validators.Entidades
{
    public static class DoctorVAlidators
    {
       
        public static string ValidateEspecialidad(this string especialidad)
        {
            var nombreParametro = "especialidad";
            especialidad = especialidad.Trim().ValidateEmpty(nombreParametro)
                                                .ValidateLength(nombreParametro, 5,100);
            
            return especialidad;
        }
    }
}
