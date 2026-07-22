using Bisoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Service;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Bisoft.Consultorio.Aplicacion.Services
{
    public class CitaService
    {
        private readonly ILogger<CitaService> _logger;
        private readonly CitaDomainService _citaDomainService;

        public CitaService(ILogger<CitaService> logger, CitaDomainService citaDomainService)
        {
            _logger = logger;
            _citaDomainService = citaDomainService;
        }

        // ===== REGISTRAR =====
        public async Task<RegistrarCitaResponse> RegistrarCita(
            Guid pacienteId,
            Guid doctorId,
            DateTime fechaHora,
            int duracionMinutos,
            string motivo,
            Guid? salaId = null,
            string? notas = null)
        {
            var cita = await _citaDomainService.RegistrarCita(
                pacienteId,
                doctorId,
                fechaHora,
                duracionMinutos,
                motivo,
                salaId,
                notas);

            _logger.LogInformation("Cita registrada: Id={CitaId}, Fecha={CitaFecha}, Motivo={CitaMotivo}",
                cita.Id, cita.FechaHora, cita.Motivo);

            return cita.Adapt<RegistrarCitaResponse>();
        }

        // ===== CONSULTAR =====
        public async Task<ConsultarCitaResponse> ConsultarCita(Guid citaId)
        {
            var cita = await _citaDomainService.ObtenerCita(citaId);
            _logger.LogInformation("Cita obtenida con id {CitaId}", citaId);
            return cita.Adapt<ConsultarCitaResponse>();
        }

        // ===== ACTUALIZAR =====
        public async Task<ActualizarCitaResponse> ActualizarCita(
            Guid citaId,
            DateTime? fechaHora = null,
            int? duracionMinutos = null,
            string? motivo = null,
            string? notas = null,
            Guid? salaId = null,
            string? motivoCancelacion = null)
        {
            var cita = await _citaDomainService.ActualizarCita(
                citaId,
                fechaHora,
                duracionMinutos,
                motivo,
                notas,
                salaId,
                motivoCancelacion);

            _logger.LogInformation("Cita actualizada con id {CitaId}", citaId);
            return cita.Adapt<ActualizarCitaResponse>();
        }

        // ===== CONFIRMAR =====
        public async Task<ConfirmarCitaResponse> ConfirmarCita(Guid citaId)
        {
            var cita = await _citaDomainService.ConfirmarCita(citaId);
            _logger.LogInformation("Cita confirmada con id {CitaId}", citaId);
            return cita.Adapt<ConfirmarCitaResponse>();
        }

        // ===== CANCELAR =====
        public async Task<CancelarCitaResponse> CancelarCita(Guid citaId, string? motivoCancelacion = null)
        {
            var cita = await _citaDomainService.CancelarCita(citaId, motivoCancelacion);
            _logger.LogInformation("Cita cancelada con id {CitaId}, Motivo: {Motivo}", citaId, motivoCancelacion);
            return cita.Adapt<CancelarCitaResponse>();
        }

        // ===== REAGENDAR =====
        public async Task<ReagendarCitaResponse> ReagendarCita(
            Guid citaId,
            DateTime nuevaFechaHora,
            int nuevaDuracionMinutos,
            Guid? salaId = null)
        {
            var cita = await _citaDomainService.ReagendarCita(
                citaId,
                nuevaFechaHora,
                nuevaDuracionMinutos,
                salaId);

            _logger.LogInformation("Cita reagendada con id {CitaId}, NuevaFecha: {NuevaFecha}", citaId, nuevaFechaHora);
            return cita.Adapt<ReagendarCitaResponse>();
        }

        // ===== ELIMINAR =====
        public async Task<EliminarCitaResponse> EliminarCita(Guid citaId)
        {
            var cita = await _citaDomainService.EliminarCita(citaId);
            _logger.LogInformation("Cita eliminada con id {CitaId}", citaId);
            return cita.Adapt<EliminarCitaResponse>();
        }

        // ===== VERIFICAR DISPONIBILIDAD =====
        public async Task<bool> VerificarDisponibilidad(
            Guid doctorId,
            Guid? salaId,
            DateTime fechaHora,
            int duracionMinutos,
            Guid? citaIdExcluir = null)
        {
            return await _citaDomainService.VerificarDisponibilidad(
                doctorId,
                salaId,
                fechaHora,
                duracionMinutos,
                citaIdExcluir);
        }
    }
}