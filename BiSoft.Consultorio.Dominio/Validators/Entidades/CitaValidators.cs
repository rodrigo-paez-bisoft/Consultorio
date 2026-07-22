using System;
using System.Collections.Generic;

namespace BiSoft.Consultorio.Dominio.Validators
{
    public static class CitaValidators
    {
        // 🔽 Cambiar new(8, 0, 0) por new TimeSpan(8, 0, 0)
        private static readonly TimeSpan HORA_INICIO = new TimeSpan(8, 0, 0);   // 8:00 AM
        private static readonly TimeSpan HORA_FIN = new TimeSpan(18, 0, 0);    // 6:00 PM

        public static void ValidarFechaHora(DateTime fechaHora)
        {
            // 1. Validar día hábil (lunes a viernes)
            if (fechaHora.DayOfWeek == DayOfWeek.Saturday ||
                fechaHora.DayOfWeek == DayOfWeek.Sunday)
                throw new ArgumentException("Las citas solo se agendan de lunes a viernes");

            // 2. Validar horario laboral
            var hora = fechaHora.TimeOfDay;
            if (hora < HORA_INICIO || hora > HORA_FIN)
                throw new ArgumentException($"El horario de atención es de {HORA_INICIO:hh\\:mm} a {HORA_FIN:hh\\:mm}");

            // 3. Validar antelación mínima (1 hora)
            if (fechaHora < DateTime.Now.AddHours(1))
                throw new ArgumentException("Las citas requieren al menos 1 hora de antelación");

            // 4. Validar antelación máxima (30 días)
            if (fechaHora > DateTime.Now.AddDays(30))
                throw new ArgumentException("No se pueden agendar citas con más de 30 días de antelación");

            // 5. Validar que no sea en el pasado
            if (fechaHora < DateTime.Now)
                throw new ArgumentException("No se pueden agendar citas en el pasado");
        }

        public static void ValidarDuracion(int duracionMinutos)
        {
            // Duraciones permitidas: 20, 30, 45, 60 minutos
            var duracionesPermitidas = new int[] { 20, 30, 45, 60 };
            if (!Array.Exists(duracionesPermitidas, d => d == duracionMinutos))
                throw new ArgumentException("La duración debe ser de 20, 30, 45 o 60 minutos");
        }

        public static void ValidarCancelacion(DateTime fechaHora)
        {
            // Se puede cancelar hasta 1 hora antes
            if (fechaHora < DateTime.Now.AddHours(1))
                throw new ArgumentException("No se puede cancelar una cita con menos de 1 hora de anticipación");
        }
    }
}