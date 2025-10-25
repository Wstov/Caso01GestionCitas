using GestionDeCitasDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasDAL.Repositorios
{
    public class VehiculosRepositorio : IVehiculosRepositorio
    {
        private readonly List<Vehiculo> _vehiculos = new()
        {
            new Vehiculo { Id = 1, Placa = "ABC123", Marca = "Toyota",  Anio = 2018, ClienteId = 1 },
            new Vehiculo { Id = 2, Placa = "XYZ456", Marca = "Hyundai", Anio = 2020, ClienteId = 2 },
        };

        public Task<List<Vehiculo>> ObtenerAsync() => Task.FromResult(_vehiculos);

        public Task<Vehiculo?> ObtenerPorIdAsync(int id) =>
            Task.FromResult(_vehiculos.FirstOrDefault(v => v.Id == id));

        public Task<Vehiculo?> ObtenerPorPlacaAsync(string placa) =>
            Task.FromResult(_vehiculos.FirstOrDefault(v => v.Placa == placa));

        public Task<bool> AgregarAsync(Vehiculo vehiculo)
        {
            vehiculo.Id = _vehiculos.Any() ? _vehiculos.Max(v => v.Id) + 1 : 1;
            _vehiculos.Add(vehiculo);
            return Task.FromResult(true);
        }

        public Task<bool> ActualizarAsync(Vehiculo vehiculo)
        {
            var db = _vehiculos.FirstOrDefault(v => v.Id == vehiculo.Id);
            if (db is null) return Task.FromResult(false);
            db.Placa = vehiculo.Placa;
            db.Marca = vehiculo.Marca;
            db.Anio = vehiculo.Anio;
            db.ClienteId = vehiculo.ClienteId;
            return Task.FromResult(true);
        }

        public Task<bool> EliminarAsync(int id)
        {
            var db = _vehiculos.FirstOrDefault(v => v.Id == id);
            if (db is null) return Task.FromResult(false);
            _vehiculos.Remove(db);
            return Task.FromResult(true);
        }
    }
}
