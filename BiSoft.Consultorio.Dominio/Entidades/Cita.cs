using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Cita
    {
        //mover nivel de acceso set
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
       // public string Diagnostico { get; set; }
        public int Status { get; set; }

        //FK
        //Paciente
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        //Doctor
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        //Sala
        public Guid SalaId { get; set; }
        public string Sala { get; set; }
        public Cita() { }
        public Cita(DateTime fecha, Paciente paciente, Doctor doctor, string motivo, string diagnostico, Sala sala)
        {
            Id = Guid.NewGuid();
            Fecha = fecha;
            Paciente = paciente;
            //PacienteId= paciete.Id;
            Motivo= motivo;
            SalaId = sala.Id;
        }
    }
}
