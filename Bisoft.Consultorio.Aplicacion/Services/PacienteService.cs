using Bisoft.Consultorio.Aplicacion.DTOs.Doctor;
using Bisoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Service;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.Services
{ 
    public class PacienteService
    {
        private readonly ILogger<PacienteService> _logger;
        private readonly PacienteDomainService _pacienteDomainService;
        public PacienteService(ILogger<PacienteService> logger, PacienteDomainService pacienteDomainService)
        {
            _logger = logger;
            _pacienteDomainService = pacienteDomainService;
        }
        public async Task<RegistrarPacienteResponse> RegistrarPaciente(string nombre)
        {
            var paciente = await _pacienteDomainService.RegistrarPaciente(nombre);
            _logger.LogInformation($"Paciente registrado: {paciente.Nombre} ");
            return paciente.Adapt<RegistrarPacienteResponse>();
        }
        public async Task<ConsultarPacienteResponse> ConsultarPaciente(Guid pacienteId)
        {
            var paciente = await _pacienteDomainService.ObtenerPaciente(pacienteId);
            _logger.LogInformation("Paciente obtenido con id {}", pacienteId);
            return paciente.Adapt<ConsultarPacienteResponse>();
        }
    }
}