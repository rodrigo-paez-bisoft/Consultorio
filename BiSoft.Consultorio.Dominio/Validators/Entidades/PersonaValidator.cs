using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Validators.Entidades
{
    public static class PersonaValidator
    {
        public static string validateNombre(this string nombre)
        {

            var nombreParametro = "nombre";
            nombre = nombre.Trim()
                .ValidateEmpty(nombreParametro)
                .ValidateLength(nombreParametro, 5, 50);
            if (!nombre.Contains(' '))
                throw new ArgumentException("El nombre debe incluir nombre y apellido");
            return nombre;
        }
    }
}
