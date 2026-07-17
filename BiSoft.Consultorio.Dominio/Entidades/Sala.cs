using BiSoft.Consultorio.Dominio.Validators.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Sala
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int Status { get; set; }
        private Sala() { }
        public Sala(string nombre)
        {
            Id= Guid.NewGuid();
            Nombre= nombre;
            Status = 1;
        }
        public void Actualizar(string nombre)
        {
            Nombre = nombre;
            
        }

    }
}
