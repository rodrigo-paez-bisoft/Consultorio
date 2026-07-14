using BiSoft.Consultorio.Dominio.Validators.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades.Base
{
    public abstract class Persona
    {
        public Guid Id { get; }
        public string Nombre { get; private set; }
        protected Persona() { }
        protected Persona(string nombre) {
            Id = Guid.NewGuid();
            Nombre = nombre.validateNombre();
        }
        public void Actualizar(string nombre)
        {
            Nombre = nombre;
        }
    }
}
