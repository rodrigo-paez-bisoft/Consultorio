using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Cita
    {
        public Guid Id { get; set; }
        public DateTime fecha { get; set; }
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string motivo { get; set; }
        public string Diagnostico { get; set; }
        public Guid SalaId { get; set; }
        public string Sala { get; set; }
    }
}
