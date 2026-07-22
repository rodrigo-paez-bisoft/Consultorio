using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.Extensions.Logging;

namespace Bisoft.Consultorio.Aplicacion.Services
{
    public class LoginAuditService
    {
        private readonly ILogger<LoginAuditService> _logger;
        private readonly ILoginAuditRepository _loginAuditRepository;

        public LoginAuditService(
            ILogger<LoginAuditService> logger,
            ILoginAuditRepository loginAuditRepository)
        {
            _logger = logger;
            _loginAuditRepository = loginAuditRepository;
        }

        public async Task<LoginAudit> RegistrarLogin(
            string usuario,
            bool exitoso,
            string? ipAddress = null)
        {
            var login = new LoginAudit
            {
                Id = Guid.NewGuid(),
                Usuario = usuario,
                Exitoso = exitoso,
                FechaHora = DateTime.Now,
                IpAddress = ipAddress
            };

            await _loginAuditRepository.RegistrarLogin(login);
            await _loginAuditRepository.GuardarCambios();

            _logger.LogInformation("Login registrado: Usuario={Usuario}, Exitoso={Exitoso}, IP={IpAddress}",
                usuario, exitoso, ipAddress);

            return login;
        }

        public async Task<int> ContarIntentosFallidos(string usuario, int minutos = 15)
        {
            var desde = DateTime.Now.AddMinutes(-minutos);
            return await _loginAuditRepository.ContarIntentosFallidos(usuario, desde);
        }

        public async Task<bool> EstaBloqueado(string usuario, int maxIntentos = 5, int minutos = 15)
        {
            var intentos = await ContarIntentosFallidos(usuario, minutos);
            return intentos >= maxIntentos;
        }
    }
}