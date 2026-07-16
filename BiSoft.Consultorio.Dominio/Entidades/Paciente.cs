using System;
using System.Collections.Generic;
using System.Text;
using BiSoft.Consultorio.Dominio.Entidades.Base;
using BiSoft.Consultorio.Dominio.Validators.Entidades;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Paciente : Persona
    {
        
        private Paciente() { }
        public Paciente(string nombre) : base(nombre)   {}
        
    }
}
