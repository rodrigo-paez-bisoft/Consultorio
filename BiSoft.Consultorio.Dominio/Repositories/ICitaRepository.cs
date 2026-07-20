using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface ICitaRepository
    {
        Task RegistrarCita(Cita cita);
        Task ActualizarCita(Cita cita);
        Task GuardarCambios();
        Task<Cita?> ObtenerCita(Guid citaId);
        IQueryable<Cita> ConsultarCitas();
        Task EliminarCita(Cita cita);
    }
}
