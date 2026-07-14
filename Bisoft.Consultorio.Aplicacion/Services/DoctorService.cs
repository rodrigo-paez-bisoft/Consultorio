using Bisoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Service;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bisoft.Consultorio.Aplicacion.Services
{
    public class DoctorService
    {
        private readonly ILogger<DoctorService> _logger;
        private readonly DoctorDomainService _doctorDomainService;
        public DoctorService(ILogger<DoctorService> logger, DoctorDomainService doctorDomainService)
        {
            _logger = logger;
            _doctorDomainService = doctorDomainService;
        }
        public async Task<RegistrarDoctorResponse> RegistrarDoctor(string nombre, string especialidad)
        {
            var doctor = await _doctorDomainService.RegistrarDoctor(nombre, especialidad);
            _logger.LogInformation("Doctor registrado: {DoctorNombre}, Especialidad: {DoctorEspecialidad}", doctor.Nombre, doctor.Especialidad);
            return doctor.Adapt<RegistrarDoctorResponse>();
        }
    }
}
