using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface ISalaRepository
    {
        Task RegistrarSala(Sala sala);
        Task GuardarCambios();
        Task<Sala?> ObtenerSala(Guid salaId);
        IQueryable<Sala> ConsultaSalas();
        Task EliminarSalas(Sala sala);

    }
}
