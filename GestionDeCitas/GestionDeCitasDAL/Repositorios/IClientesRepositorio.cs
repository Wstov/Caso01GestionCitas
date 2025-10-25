using GestionDeCitasDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasDAL.Repositorios
{
    public interface IClientesRepositorio
    {
        Task<List<Cliente>> ObtenerAsync();
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion);
        Task<bool> AgregarAsync(Cliente cliente);
        Task<bool> ActualizarAsync(Cliente cliente);
        Task<bool> EliminarAsync(int id);
    }
}
