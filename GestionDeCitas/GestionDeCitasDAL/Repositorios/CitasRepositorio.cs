using GestionDeCitasDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasDAL.Repositorios
{
    public class CitasRepositorio : ICitasRepositorio
    {
        private readonly List<Cita> _citas = new();

        public Task<List<Cita>> ObtenerAsync() => Task.FromResult(_citas);

        public Task<Cita?> ObtenerPorIdAsync(int id) =>
            Task.FromResult(_citas.FirstOrDefault(c => c.Id == id));

        public Task<bool> AgregarAsync(Cita cita)
        {
            cita.Id = _citas.Any() ? _citas.Max(c => c.Id) + 1 : 1;
            _citas.Add(cita);
            return Task.FromResult(true);
        }

        public Task<bool> ActualizarAsync(Cita cita)
        {
            var db = _citas.FirstOrDefault(c => c.Id == cita.Id);
            if (db is null) return Task.FromResult(false);
            db.ClienteId = cita.ClienteId;
            db.VehiculoId = cita.VehiculoId;
            db.Fecha = cita.Fecha;
            db.Estado = cita.Estado;
            return Task.FromResult(true);
        }

        public Task<bool> EliminarAsync(int id)
        {
            var db = _citas.FirstOrDefault(c => c.Id == id);
            if (db is null) return Task.FromResult(false);
            _citas.Remove(db);
            return Task.FromResult(true);
        }
    }
}
