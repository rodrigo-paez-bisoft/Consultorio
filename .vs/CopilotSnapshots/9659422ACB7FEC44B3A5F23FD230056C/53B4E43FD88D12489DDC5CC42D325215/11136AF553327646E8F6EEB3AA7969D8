using Bisoft.Consultorio.Aplicacion.DTOs.Paciente;
using Bisoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Service;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.Services
{
    public class SalaService
    {
        private readonly ILogger<SalaService> _logger;
        private readonly SalaDomainService _salaDomainService;
        public SalaService(ILogger<SalaService> logger, SalaDomainService salaDomainService)
        {
            _logger = logger;
            _salaDomainService= salaDomainService;
        }
        public async Task<RegistraSalaResponse> RegistrarSala(string nombre)
        {
            var sala = await _salaDomainService.RegistraSala(nombre);
            _logger.LogInformation($"Sala registrada: {sala.Nombre}");
            return sala.Adapt<RegistraSalaResponse>();
        }
        public async Task<ActualizarSalaResponse> ActualizarSala(Guid salaId, string nombre)
        {
            var sala= await _salaDomainService.ActualizaSala(salaId, nombre);
            _logger.LogInformation($"Sala actualizada con id {salaId}");
            return sala.Adapt<ActualizarSalaResponse>();
        }
        public async Task<ConsultaSalaResponse> ConsultarSala(Guid salaId)
        {
            var sala = await _salaDomainService.ObtenerSala(salaId);
            _logger.LogInformation($"Sala obtenida con id {salaId}");
            return sala.Adapt<ConsultaSalaResponse>();
        }
        public async Task<EliminarSalaResponse> EliminarSala(Guid salaId)
        {
            var sala= await _salaDomainService.EliminaSala(salaId);
            _logger.LogInformation($"Sala eliminada con id {salaId}");
            return sala.Adapt<EliminarSalaResponse>();
        }
    }
}
