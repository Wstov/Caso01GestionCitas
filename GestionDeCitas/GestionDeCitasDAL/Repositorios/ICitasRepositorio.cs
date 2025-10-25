using GestionDeCitasDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasDAL.Repositorios
{
    public interface ICitasRepositorio
    {
        Task<List<Cita>> ObtenerAsync();
        Task<Cita?> ObtenerPorIdAsync(int id);
        Task<bool> AgregarAsync(Cita cita);
        Task<bool> ActualizarAsync(Cita cita);
        Task<bool> EliminarAsync(int id);
    }
}
