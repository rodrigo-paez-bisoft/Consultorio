using Bisoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Service;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

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

        public async Task<RegistrarCitaResponse> RegistrarCita(DateTime fecha, string motivo, int status, Guid pacienteId, Guid doctorId, Guid salaId, string sala)
        {
            var cita = await _citaDomainService.RegistrarCita(fecha, motivo, status, pacienteId, doctorId, salaId, sala);
            _logger.LogInformation("Cita registrada: Id={CitaId}, Fecha={CitaFecha}, Motivo={CitaMotivo}", cita.Id, cita.Fecha, cita.Motivo);
            return cita.Adapt<RegistrarCitaResponse>();
        }

        public async Task<ActualizarCitaResponse> ActualizarCita(Guid citaId, DateTime fecha, string motivo, int status, Guid salaId, string sala)
        {
            var cita = await _citaDomainService.ActualizarCita(citaId, fecha, motivo, status, salaId, sala);
            _logger.LogInformation($"Cita actualizada con id {citaId}");
            return cita.Adapt<ActualizarCitaResponse>();
        }

        public async Task<ConsultarCitaResponse> ConsultarCita(Guid citaId)
        {
            var cita = await _citaDomainService.ObtenerCita(citaId);
            _logger.LogInformation($"Cita obtenida con id {citaId}");
            return cita.Adapt<ConsultarCitaResponse>();
        }

        public async Task<EliminarCitaResponse> EliminarCita(Guid citaId)
        {
            var cita = await _citaDomainService.EliminarCita(citaId);
            _logger.LogInformation($"Cita eliminada con id {citaId}");
            return cita.Adapt<EliminarCitaResponse>();
        }
    }
}
