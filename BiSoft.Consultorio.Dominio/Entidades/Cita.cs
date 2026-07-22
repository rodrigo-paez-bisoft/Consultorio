using BiSoft.Consultorio.Dominio.Validators;
using System;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Cita
    {
        public Guid Id { get; private set; }
        public DateTime FechaHora { get; private set; }
        public int DuracionMinutos { get; private set; }
        public string Motivo { get; private set; } = string.Empty;
        public string? Notas { get; private set; }
        public int Status { get; private set; }  // 1=Pendiente, 2=Confirmada, 3=Completada, 4=Cancelada, 5=NoAsistio
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaCancelacion { get; private set; }
        public string? MotivoCancelacion { get; private set; }

        // FK - Paciente
        public Guid PacienteId { get; private set; }
        public Paciente? Paciente { get; private set; }

        // FK - Doctor
        public Guid DoctorId { get; private set; }
        public Doctor? Doctor { get; private set; }

        // FK - Sala (opcional)
        public Guid? SalaId { get; private set; }
        public Sala? Sala { get; private set; }

        // Constructor privado para EF Core
        private Cita() { }

        public Cita(
            Guid pacienteId,
            Guid doctorId,
            DateTime fechaHora,
            int duracionMinutos,
            string motivo,
            Guid? salaId = null,
            string? notas = null)
        {
            // Validaciones
            CitaValidators.ValidarFechaHora(fechaHora);
            CitaValidators.ValidarDuracion(duracionMinutos);

            Id = Guid.NewGuid();
            PacienteId = pacienteId;
            DoctorId = doctorId;
            SalaId = salaId;
            FechaHora = fechaHora;
            DuracionMinutos = duracionMinutos;
            Motivo = motivo.ValidateEmpty("motivo").ValidateLength("motivo", 3, 500);
            Notas = notas?.ValidateLength("notas", 0, 1000);
            Status = (int)EstadoCita.Pendiente;
            FechaCreacion = DateTime.Now;
        }

        // Métodos de negocio
        public void Confirmar()
        {
            if (Status != (int)EstadoCita.Pendiente)
                throw new InvalidOperationException("Solo se pueden confirmar citas pendientes");

            if (FechaHora < DateTime.Now.AddHours(1))
                throw new InvalidOperationException("No se puede confirmar una cita con menos de 1 hora de anticipación");

            Status = (int)EstadoCita.Confirmada;
        }

        public void Completar()
        {
            if (Status != (int)EstadoCita.Confirmada)
                throw new InvalidOperationException("Solo se pueden completar citas confirmadas");

            Status = (int)EstadoCita.Completada;
        }

        public void Cancelar(string? motivo = null)
        {
            if (Status == (int)EstadoCita.Completada)
                throw new InvalidOperationException("No se puede cancelar una cita completada");

            if (Status == (int)EstadoCita.Cancelada)
                throw new InvalidOperationException("La cita ya está cancelada");

            // Si se cancela con menos de 1 hora, se marca como No Asistió
            if (FechaHora < DateTime.Now.AddHours(1))
            {
                Status = (int)EstadoCita.NoAsistio;
                MotivoCancelacion = motivo ?? "Cancelación tardía (menos de 1 hora)";
            }
            else
            {
                Status = (int)EstadoCita.Cancelada;
                MotivoCancelacion = motivo;
            }

            FechaCancelacion = DateTime.Now;
        }

        public void Reagendar(DateTime nuevaFechaHora, int nuevaDuracion)
        {
            if (Status == (int)EstadoCita.Completada || Status == (int)EstadoCita.Cancelada)
                throw new InvalidOperationException("No se puede reagendar una cita completada o cancelada");

            CitaValidators.ValidarFechaHora(nuevaFechaHora);
            CitaValidators.ValidarDuracion(nuevaDuracion);

            FechaHora = nuevaFechaHora;
            DuracionMinutos = nuevaDuracion;
            Status = (int)EstadoCita.Pendiente; // Vuelve a pendiente para reconfirmar
        }

        public void ActualizarMotivo(string nuevoMotivo)
        {
            Motivo = nuevoMotivo.ValidateEmpty("motivo").ValidateLength("motivo", 3, 500);
        }

        public void ActualizarNotas(string? notas)
        {
            Notas = notas?.ValidateLength("notas", 0, 1000);
        }
    }

    public enum EstadoCita
    {
        Pendiente = 1,
        Confirmada = 2,
        Completada = 3,
        Cancelada = 4,
        NoAsistio = 5
    }
}