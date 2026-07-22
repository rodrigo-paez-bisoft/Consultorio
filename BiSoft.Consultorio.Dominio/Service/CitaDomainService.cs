using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Service
{
    public class CitaDomainService
    {
        private readonly ILogger<CitaDomainService> _logger;
        private readonly ICitaRepository _citaRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ISalaRepository _salaRepository;

        public CitaDomainService(
            ICitaRepository citaRepository,
            IDoctorRepository doctorRepository,
            IPacienteRepository pacienteRepository,
            ISalaRepository salaRepository,
            ILogger<CitaDomainService> logger)
        {
            _citaRepository = citaRepository;
            _doctorRepository = doctorRepository;
            _pacienteRepository = pacienteRepository;
            _salaRepository = salaRepository;
            _logger = logger;
        }

        // ===== REGISTRAR =====
        public async Task<Cita> RegistrarCita(
            Guid pacienteId,
            Guid doctorId,
            DateTime fechaHora,
            int duracionMinutos,
            string motivo,
            Guid? salaId = null,
            string? notas = null)
        {
            var paciente = await _pacienteRepository.ObtenerPaciente(pacienteId)
                ?? throw new KeyNotFoundException($"Paciente con ID {pacienteId} no encontrado");

            var doctor = await _doctorRepository.ObtenerDoctor(doctorId)
                ?? throw new KeyNotFoundException($"Doctor con ID {doctorId} no encontrado");

            if (salaId.HasValue)
            {
                var sala = await _salaRepository.ObtenerSala(salaId.Value)
                    ?? throw new KeyNotFoundException($"Sala con ID {salaId} no encontrada");
            }

            await ValidarDisponibilidad(doctorId, salaId, fechaHora, duracionMinutos);

            var cita = new Cita(
                pacienteId,
                doctorId,
                fechaHora,
                duracionMinutos,
                motivo,
                salaId,
                notas);

            await _citaRepository.RegistrarCita(cita);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita registrada: Id={CitaId}, Doctor={DoctorId}, Paciente={PacienteId}, Fecha={Fecha}",
                cita.Id, doctorId, pacienteId, fechaHora);

            return cita;
        }

        // ===== CONSULTAR =====
        public async Task<Cita> ObtenerCita(Guid citaId)
        {
            var cita = await _citaRepository.ObtenerCita(citaId)
                ?? throw new KeyNotFoundException($"No se encontró la cita con ID {citaId}");

            _logger.LogInformation("Cita obtenida: Id={CitaId}", citaId);
            return cita;
        }

        public IQueryable<Cita> ConsultarCitas()
        {
            var citas = _citaRepository.ConsultarCitas();
            _logger.LogInformation("Citas consultadas: Total={Count}", citas.Count());
            return citas;
        }

        // ===== ACTUALIZAR =====
        public async Task<Cita> ActualizarCita(
            Guid citaId,
            DateTime? fechaHora = null,
            int? duracionMinutos = null,
            string? motivo = null,
            string? notas = null,
            Guid? salaId = null,
            string? motivoCancelacion = null)
        {
            var cita = await ObtenerCita(citaId);

            if (cita.Status == (int)EstadoCita.Completada || cita.Status == (int)EstadoCita.Cancelada)
                throw new InvalidOperationException("No se puede actualizar una cita completada o cancelada");

            if (fechaHora.HasValue && duracionMinutos.HasValue)
            {
                await ValidarDisponibilidad(
                    cita.DoctorId,
                    salaId ?? cita.SalaId,
                    fechaHora.Value,
                    duracionMinutos.Value,
                    citaId);

                cita.Reagendar(fechaHora.Value, duracionMinutos.Value);
            }

            if (!string.IsNullOrWhiteSpace(motivo))
                cita.ActualizarMotivo(motivo);

            if (notas != null)
                cita.ActualizarNotas(notas);

            if (salaId.HasValue && salaId != cita.SalaId)
            {
                var sala = await _salaRepository.ObtenerSala(salaId.Value)
                    ?? throw new KeyNotFoundException($"Sala con ID {salaId} no encontrada");
            }

            if (!string.IsNullOrWhiteSpace(motivoCancelacion))
            {
                cita.Cancelar(motivoCancelacion);
            }

            await _citaRepository.ActualizarCita(cita);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita actualizada: Id={CitaId}", citaId);
            return cita;
        }

        // ===== REAGENDAR =====
        public async Task<Cita> ReagendarCita(
            Guid citaId,
            DateTime nuevaFechaHora,
            int nuevaDuracionMinutos,
            Guid? salaId = null)
        {
            var cita = await ObtenerCita(citaId);

            if (cita.Status == (int)EstadoCita.Completada || cita.Status == (int)EstadoCita.Cancelada)
                throw new InvalidOperationException("No se puede reagendar una cita completada o cancelada");

            await ValidarDisponibilidad(
                cita.DoctorId,
                salaId ?? cita.SalaId,
                nuevaFechaHora,
                nuevaDuracionMinutos,
                citaId);

            cita.Reagendar(nuevaFechaHora, nuevaDuracionMinutos);

            if (salaId.HasValue && salaId != cita.SalaId)
            {
                var sala = await _salaRepository.ObtenerSala(salaId.Value)
                    ?? throw new KeyNotFoundException($"Sala con ID {salaId} no encontrada");
            }

            await _citaRepository.ActualizarCita(cita);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita reagendada: Id={CitaId}, NuevaFecha={NuevaFecha}", citaId, nuevaFechaHora);
            return cita;
        }

        // ===== CAMBIAR ESTADO =====
        public async Task<Cita> ConfirmarCita(Guid citaId)
        {
            var cita = await ObtenerCita(citaId);
            cita.Confirmar();

            await _citaRepository.ActualizarCita(cita);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita confirmada: Id={CitaId}", citaId);
            return cita;
        }

        public async Task<Cita> CompletarCita(Guid citaId)
        {
            var cita = await ObtenerCita(citaId);
            cita.Completar();

            await _citaRepository.ActualizarCita(cita);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita completada: Id={CitaId}", citaId);
            return cita;
        }

        public async Task<Cita> CancelarCita(Guid citaId, string? motivo = null)
        {
            var cita = await ObtenerCita(citaId);
            cita.Cancelar(motivo);

            await _citaRepository.ActualizarCita(cita);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita cancelada: Id={CitaId}, Motivo={Motivo}", citaId, motivo);
            return cita;
        }

        // ===== ELIMINAR =====
        public async Task<Cita> EliminarCita(Guid citaId)
        {
            var cita = await ObtenerCita(citaId);
            await _citaRepository.EliminarCita(cita);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita eliminada: Id={CitaId}", citaId);
            return cita;
        }

        // ===== VALIDAR DISPONIBILIDAD =====
        public async Task<bool> VerificarDisponibilidad(
            Guid doctorId,
            Guid? salaId,
            DateTime fechaHora,
            int duracionMinutos,
            Guid? citaIdExcluir = null)
        {
            try
            {
                await ValidarDisponibilidad(doctorId, salaId, fechaHora, duracionMinutos, citaIdExcluir);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task ValidarDisponibilidad(
            Guid doctorId,
            Guid? salaId,
            DateTime fechaHora,
            int duracionMinutos,
            Guid? citaIdExcluir = null)
        {
            var fin = fechaHora.AddMinutes(duracionMinutos);

            var citasDoctor = _citaRepository.ConsultarCitas()
                .Where(c => c.DoctorId == doctorId
                    && c.Status != (int)EstadoCita.Cancelada
                    && c.Status != (int)EstadoCita.NoAsistio
                    && ((c.FechaHora >= fechaHora && c.FechaHora < fin)
                        || (fechaHora >= c.FechaHora && fechaHora < c.FechaHora.AddMinutes(c.DuracionMinutos))));

            if (citaIdExcluir.HasValue)
                citasDoctor = citasDoctor.Where(c => c.Id != citaIdExcluir.Value);

            if (citasDoctor.Any())
                throw new InvalidOperationException("El doctor ya tiene una cita en ese horario");

            if (salaId.HasValue)
            {
                var citasSala = _citaRepository.ConsultarCitas()
                    .Where(c => c.SalaId == salaId.Value
                        && c.Status != (int)EstadoCita.Cancelada
                        && c.Status != (int)EstadoCita.NoAsistio
                        && ((c.FechaHora >= fechaHora && c.FechaHora < fin)
                            || (fechaHora >= c.FechaHora && fechaHora < c.FechaHora.AddMinutes(c.DuracionMinutos))));

                if (citaIdExcluir.HasValue)
                    citasSala = citasSala.Where(c => c.Id != citaIdExcluir.Value);

                if (citasSala.Any())
                    throw new InvalidOperationException("La sala ya está ocupada en ese horario");
            }
        }
    }
}