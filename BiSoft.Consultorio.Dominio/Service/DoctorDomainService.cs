using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BiSoft.Consultorio.Dominio.Service
{
    public class DoctorDomainService
    {
        private readonly ILogger<DoctorDomainService> _logger;
        private readonly IDoctorRepository _doctorRepository;
        public DoctorDomainService(IDoctorRepository doctorRepository, ILogger<DoctorDomainService> logger)
        {
            _doctorRepository = doctorRepository;
            _logger = logger;
        }
        public async Task<Doctor> RegistrarDoctor(string nombre, string especialidad)
        {
            var doctor = new Entidades.Doctor(nombre, especialidad);
            // Aquí puedes agregar lógica adicional, como guardar el doctor en una base de datos
            await _doctorRepository.RegistrarDoctor(doctor);
            await _doctorRepository.GuardarCambios();
            _logger.LogInformation("Doctor registrado: {DoctorNombre}, Especialidad: {DoctorEspecialidad}", doctor.Nombre, doctor.Especialidad);
            return doctor;
        }
        public async Task<Doctor> ActualizarDoctor(Guid doctorId, string nombre, string especialidad)
        {
            var doctor = await ObtenerDoctor(doctorId);
            doctor.Actualizar(nombre, especialidad);
            await _doctorRepository.GuardarCambios();
            _logger.LogInformation("Doctor actualizado: {DoctorNombre}, Especialidad: {DoctorEspecialidad}", doctor.Nombre, doctor.Especialidad);
            return doctor;
        }
        public async Task<Doctor> ObtenerDoctor(Guid doctorId)
        {
            var doctor = await _doctorRepository.ObtenerDoctor(doctorId) ?? throw new KeyNotFoundException($"No se encontro el doctor con id {doctorId}");
            _logger.LogInformation("Doctor obtenido: {DoctorNombre}, Especialidad: {DoctorEspecialidad}", doctor.Nombre, doctor.Especialidad);
            return doctor;
        }
        public IQueryable<Doctor> ConsultarDoctores()
        {
            var doctores = _doctorRepository.ConsutarDoctores();
            _logger.LogInformation($"Doctores consultados: {doctores.Count()}");
            return doctores;
        }
        public async Task<Doctor> EliminarDoctor(Guid doctorId)
        {

            var doctor = await ObtenerDoctor(doctorId);
            await _doctorRepository.EliminarDoctor(doctor);
            await _doctorRepository.GuardarCambios();
            _logger.LogInformation("Doctor eliminado: {DoctorNombre}, Especialidad: {DoctorEspecialidad}", doctor.Nombre, doctor.Especialidad);
            return doctor;
        }
    }
}
