using BiSoft.Consultorio.Dominio.Entidades.Base;
using BiSoft.Consultorio.Dominio.Validators.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Doctor : Persona
    {
        
        public string Especialidad { get; private set; }
        private Doctor() : base() { }
        public Doctor(string nombre, string especialidad) : base(nombre)
        {
            Especialidad = especialidad.ValidateEspecialidad();
        }
        public void Actualizar(string nombre, string especialidad)
        {
            Actualizar(nombre);
            Especialidad= especialidad.ValidateEspecialidad();
        }
    }
}
