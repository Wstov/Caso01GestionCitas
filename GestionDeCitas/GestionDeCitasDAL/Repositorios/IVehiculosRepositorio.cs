using GestionDeCitasDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasDAL.Repositorios
{
    public interface IVehiculosRepositorio
    {
        Task<List<Vehiculo>> ObtenerAsync();
        Task<Vehiculo?> ObtenerPorIdAsync(int id);
        Task<Vehiculo?> ObtenerPorPlacaAsync(string placa);
        Task<bool> AgregarAsync(Vehiculo vehiculo);
        Task<bool> ActualizarAsync(Vehiculo vehiculo);
        Task<bool> EliminarAsync(int id);
    }
}
